using Entities.EntityClass;
using FollowUp.Domain.Repositories.FollowUp;
using FollowUp.Dtos.RequestDto.FollowUp;
using FollowUp.Dtos.RequestDto.FollowUpDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace FollowUp.Application.Services
{
    public class FollowUpService
    {
        private readonly IFollowUpQueryRepository _followUpQueryRepository;
        private readonly IFollowUpCommandRepository _followUpCommandRepository;
        private readonly MapperService _mapperService;

        public FollowUpService(IFollowUpQueryRepository followUpQueryRepository, IFollowUpCommandRepository followUpCommandRepository,
            MapperService mapperService)
        {
            _followUpQueryRepository = followUpQueryRepository;
            _followUpCommandRepository = followUpCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.FollowUp>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.FollowUp>>();

            try
            {
                var followUps = await _followUpQueryRepository.GetAll();

                if (followUps == null)
                {
                    ResponseHelper.SetFailedResponse(response, followUps.Result, followUps.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, followUps.Result, followUps.Message, StatusResponseMessage.success, followUps.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the  follow-ups.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.FollowUp>>> GetBookMarks()
        {
            var response = new Response<List<Entities.EntityClass.FollowUp>>();

            try
            {
                var followUps = await _followUpQueryRepository.GetBookMarks();

                if (followUps == null)
                {
                    ResponseHelper.SetFailedResponse(response, followUps.Result, followUps.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, followUps.Result, followUps.Message, StatusResponseMessage.success, followUps.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the  follow-ups.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        

        public async Task<Response<Entities.EntityClass.FollowUp>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.FollowUp>();

            try
            {
                var followUp = await _followUpQueryRepository.GetById(id);

                if (followUp == null)
                {
                    ResponseHelper.SetFailedResponse(response, followUp.Result, followUp.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, followUp.Result, followUp.Message, StatusResponseMessage.success, followUp.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the  follow-up.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(FollowUpInsertRequestDto followUpDto)
        {
            var response = new Response<int>();

            try
            {
                var followUpEntity = await _mapperService.MapSingle<FollowUpInsertRequestDto, Entities.EntityClass.FollowUp>(followUpDto);

                var insertResponse = await _followUpCommandRepository.Insert(followUpEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the patient follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the patient follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(FollowUpUpdateRequestDto followUpDto)
        {
            var response = new Response<int>();

            try
            {
                var followUpEntity = await _mapperService.MapSingle<FollowUpUpdateRequestDto, Entities.EntityClass.FollowUp>(followUpDto);
                followUpEntity.UpdatedAt = DateTime.Now;

                var updatedResponse = await _followUpCommandRepository.Update(followUpEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the patient follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the patient follow-up.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _followUpCommandRepository.Delete(id);
        }
        public Task<Response<List<Entities.EntityClass.FollowUp>>> GetAllByName(string followup)
        {
            return _followUpQueryRepository.GetAllFollowUpByName(followup);
        }
    }
}
