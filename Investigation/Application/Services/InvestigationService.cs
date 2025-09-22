using Investigation.Domain.Repositories.Investigation;
using Investigation.Dtos.RequestDto.InvestigationDto;
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

namespace Investigation.Application.Services
{
    public class InvestigationService
    {
        private readonly IInvestigationQueryRepository _investigationQueryRepository;
        private readonly IInvestigationCommandRepository _investigationCommandRepository;
        private readonly MapperService _mapperService;
        public InvestigationService(IInvestigationQueryRepository investigationQueryRepository, IInvestigationCommandRepository investigationCommandRepository,
            MapperService mapperService)
        {
            _investigationQueryRepository = investigationQueryRepository;
            _investigationCommandRepository = investigationCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Investigation>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Investigation>>();

            try
            {
                var investigation = await _investigationQueryRepository.GetAll();

                if (investigation == null)
                {
                    ResponseHelper.SetFailedResponse(response, investigation.Result, investigation.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, investigation.Result, investigation.Message, StatusResponseMessage.success, investigation.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the investigation.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Investigation>>> GetBookMarks()
        {
            var response = new Response<List<Entities.EntityClass.Investigation>>();

            try
            {
                var investigation = await _investigationQueryRepository.GetBookMarks();

                if (investigation == null)
                {
                    ResponseHelper.SetFailedResponse(response, investigation.Result, investigation.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, investigation.Result, investigation.Message, StatusResponseMessage.success, investigation.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the investigation.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        

        public async Task<Response<Entities.EntityClass.Investigation>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Investigation>();

            try
            {
                var investigation = await _investigationQueryRepository.GetById(id);

                if (investigation == null)
                {
                    ResponseHelper.SetFailedResponse(response, investigation.Result, investigation.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, investigation.Result, investigation.Message, StatusResponseMessage.success, investigation.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the investigation.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(InvestigationInsertRequestDto investigation)
        {
            var response = new Response<int>();

            try
            {
                var investigationEntity = await _mapperService.MapSingle<InvestigationInsertRequestDto, Entities.EntityClass.Investigation>(investigation);
                var insertResponse = await _investigationCommandRepository.Insert(investigationEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(InvestigationUpdateRequestDto investigation)
        {
            var response = new Response<int>();

            try
            {
                var investigationEntity = await _mapperService.MapSingle<InvestigationUpdateRequestDto, Entities.EntityClass.Investigation>(investigation);
                investigationEntity.UpdatedAt = DateTime.Now;
                investigationEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _investigationCommandRepository.Update(investigationEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the investigation.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _investigationCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.Investigation>>> GetAllInvestigationByName(string? investigationName)
        {
            return await _investigationQueryRepository.GetAllInvestigationByName(investigationName);
        }

    }
}
