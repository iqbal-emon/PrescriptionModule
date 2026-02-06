using Doctor.Domain.Repositories.CampaignDoctor;
using Doctor.Dtos.RequestDto.CampaignDoctorDto;
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

namespace Doctor.Application.Services
{
    public class CampaignDoctorService
    {
        private readonly ICampaignDoctorQueryRepository _campaignDoctorQueryRepository;
        private readonly ICampaignDoctorCommandRepository _campaignDoctorCommandRepository;
        private readonly MapperService _mapperService;

        public CampaignDoctorService(
            ICampaignDoctorQueryRepository campaignDoctorQueryRepository,
            ICampaignDoctorCommandRepository campaignDoctorCommandRepository,
            MapperService mapperService)
        {
            _campaignDoctorQueryRepository = campaignDoctorQueryRepository;
            _campaignDoctorCommandRepository = campaignDoctorCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.CampaignDoctor>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.CampaignDoctor>>();

            try
            {
                var campaignDoctors = await _campaignDoctorQueryRepository.GetAll();

                if (campaignDoctors == null)
                {
                    ResponseHelper.SetFailedResponse(response, campaignDoctors.Result, campaignDoctors.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, campaignDoctors.Result, campaignDoctors.Message, StatusResponseMessage.success, campaignDoctors.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the CampaignDoctors.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.CampaignDoctor>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.CampaignDoctor>();

            try
            {
                var campaignDoctor = await _campaignDoctorQueryRepository.GetById(id);

                if (campaignDoctor == null)
                {
                    ResponseHelper.SetFailedResponse(response, campaignDoctor.Result, campaignDoctor.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, campaignDoctor.Result, campaignDoctor.Message, StatusResponseMessage.success, campaignDoctor.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the CampaignDoctor.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(CampaignDoctorInsertRequestDto campaignDoctor)
        {
            var response = new Response<int>();

            try
            {
                var campaignDoctorEntity = await _mapperService.MapSingle<CampaignDoctorInsertRequestDto, Entities.EntityClass.DoctorEntity.CampaignDoctor>(campaignDoctor);
                campaignDoctorEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _campaignDoctorCommandRepository.Insert(campaignDoctorEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the CampaignDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the CampaignDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(CampaignDoctorUpdateRequestDto campaignDoctor)
        {
            var response = new Response<int>();

            try
            {
                var campaignDoctorEntity = await _mapperService.MapSingle<CampaignDoctorUpdateRequestDto, Entities.EntityClass.DoctorEntity.CampaignDoctor>(campaignDoctor);
                campaignDoctorEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _campaignDoctorCommandRepository.Update(campaignDoctorEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the CampaignDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the CampaignDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _campaignDoctorCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.CampaignDoctor>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.CampaignDoctor>>();

            try
            {
                var campaignDoctors = await _campaignDoctorQueryRepository.GetByDoctorId(doctorId);

                if (campaignDoctors == null)
                {
                    ResponseHelper.SetFailedResponse(response, campaignDoctors.Result, campaignDoctors.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, campaignDoctors.Result, campaignDoctors.Message, StatusResponseMessage.success, campaignDoctors.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the CampaignDoctors by doctor ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}

