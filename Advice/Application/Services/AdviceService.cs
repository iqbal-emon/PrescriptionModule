using Advice.Domain.Repositories.Advice;
using Advice.Dtos.RequestDto.AdviceDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Advice.Application.Services
{
    public class AdviceService
    {
        private readonly IAdviceQueryRepository _adviceQueryRepository;
        private readonly IAdviceCommandRepository _adviceCommandRepository;
        private readonly MapperService _mapperService;

        public AdviceService(IAdviceQueryRepository adviceQueryRepository, IAdviceCommandRepository adviceCommandRepository,
            MapperService mapperService)
        {
            _adviceQueryRepository = adviceQueryRepository;
            _adviceCommandRepository = adviceCommandRepository;
            _mapperService = mapperService;
        }

        // Get all advice records
        public async Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.CommonAdvice>>();

            try
            {
                var adviceRecords = await _adviceQueryRepository.GetAll();

                if (adviceRecords == null)
                {
                    ResponseHelper.SetFailedResponse(response, adviceRecords.Result, adviceRecords.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, adviceRecords.Result, adviceRecords.Message, StatusResponseMessage.success, adviceRecords.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the advice records.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        //Get bookmarks Advice


        public async Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.CommonAdvice>>();

            try
            {
                var adviceRecords = await _adviceQueryRepository.GetBookMarks(doctorId);

                if (adviceRecords == null)
                {
                    ResponseHelper.SetFailedResponse(response, adviceRecords.Result, adviceRecords.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, adviceRecords.Result, adviceRecords.Message, StatusResponseMessage.success, adviceRecords.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the advice records.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }



        // Get advice by ID
        public async Task<Response<Entities.EntityClass.CommonAdvice>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.CommonAdvice>();

            try
            {
                var advice = await _adviceQueryRepository.GetById(id);

                if (advice == null)
                {
                    ResponseHelper.SetFailedResponse(response, advice.Result, advice.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, advice.Result, advice.Message, StatusResponseMessage.success, advice.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the advice record.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Insert a new advice record
        public async Task<Response<int>> Insert(AdviceInsertRequestDto advice)
        {
            var response = new Response<int>();

            try
            {
                var adviceEntity = await _mapperService.MapSingle<AdviceInsertRequestDto, Entities.EntityClass.CommonAdvice>(advice);

                var insertResponse = await _adviceCommandRepository.Insert(adviceEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the advice record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the advice record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Update an existing advice record
        public async Task<Response<int>> Update(AdviceUpdateRequestDto advice)
        {
            var response = new Response<int>();

            try
            {
                var adviceEntity = await _mapperService.MapSingle<AdviceUpdateRequestDto, Entities.EntityClass.CommonAdvice>(advice);
                adviceEntity.UpdatedAt = DateTime.Now;
                adviceEntity.CreatedAt = DateTime.Now;

                var updatedResponse = await _adviceCommandRepository.Update(adviceEntity);
                response = updatedResponse;

            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the advice record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the advice record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Delete an advice record
        public Task<Response<bool>> Delete(int id)
        {
            return _adviceCommandRepository.Delete(id);
        }

        // Get all advice records by name
        public Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetAllAdviceByName(string adviceName)
        {
            return _adviceQueryRepository.GetAllAdviceByName(adviceName);
        }
    }
}
