using CommonHistory.Domain.Repositories.CommonHistory;
using CommonHistory.Dtos.RequestDto.CommonHistoryDto;
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

namespace CommonHistory.Application.Services
{
    public class CommonHistoryService
    {
        private readonly ICommonHistoryQueryRepository _commonHistoryQueryRepository;
        private readonly ICommonHistoryCommandRepository _commonHistoryCommandRepository;
        private readonly MapperService _mapperService;

        public CommonHistoryService(ICommonHistoryQueryRepository commonHistoryQueryRepository, ICommonHistoryCommandRepository commonHistoryCommandRepository,
            MapperService mapperService)
        {
            _commonHistoryQueryRepository = commonHistoryQueryRepository;
            _commonHistoryCommandRepository = commonHistoryCommandRepository;
            _mapperService = mapperService;
        }

        // Get all common history records
        public async Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.CommonHistory>>();

            try
            {
                var commonHistories = await _commonHistoryQueryRepository.GetAll();

                if (commonHistories == null)
                {
                    ResponseHelper.SetFailedResponse(response, commonHistories.Result, commonHistories.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, commonHistories.Result, commonHistories.Message, StatusResponseMessage.success, commonHistories.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the common history records.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Get common history Bookmarks 

        public async Task<Response<List<Entities.EntityClass.CommonHistory>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.CommonHistory>>();

            try
            {
                var commonHistories = await _commonHistoryQueryRepository.GetBookMarks(doctorId);

                if (commonHistories == null)
                {
                    ResponseHelper.SetFailedResponse(response, commonHistories.Result, commonHistories.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, commonHistories.Result, commonHistories.Message, StatusResponseMessage.success, commonHistories.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the common history records.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        
        // Get common history by ID
        public async Task<Response<Entities.EntityClass.CommonHistory>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.CommonHistory>();

            try
            {
                var commonHistory = await _commonHistoryQueryRepository.GetById(id);

                if (commonHistory == null)
                {
                    ResponseHelper.SetFailedResponse(response, commonHistory.Result, commonHistory.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, commonHistory.Result, commonHistory.Message, StatusResponseMessage.success, commonHistory.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the common history record.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Insert a new common history record
        public async Task<Response<int>> Insert(CommonHistoryInsertRequestDto commonHistory)
        {
            var response = new Response<int>();

            try
            {
                var commonHistoryEntity = await _mapperService.MapSingle<CommonHistoryInsertRequestDto, Entities.EntityClass.CommonHistory>(commonHistory);

                var insertResponse = await _commonHistoryCommandRepository.Insert(commonHistoryEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the common history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the common history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Update an existing common history record
        public async Task<Response<int>> Update(CommonHistoryUpdateRequestDto commonHistory)
        {
            var response = new Response<int>();

            try
            {
                var commonHistoryEntity = await _mapperService.MapSingle<CommonHistoryUpdateRequestDto, Entities.EntityClass.CommonHistory>(commonHistory);
                commonHistoryEntity.UpdatedAt = DateTime.Now;
                commonHistoryEntity.CreatedAt = DateTime.Now;

                var updatedResponse = await _commonHistoryCommandRepository.Update(commonHistoryEntity);
                response = updatedResponse;

            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the common history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the common history record.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Delete a common history record
        public Task<Response<bool>> Delete(int id)
        {
            return _commonHistoryCommandRepository.Delete(id);
        }

        // Get all common history records by name
        public Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAllCommonHistoryByName(string commonHistoryName)
        {
            return _commonHistoryQueryRepository.GetAllCommonHistoryByName(commonHistoryName);
        }
    }
}
