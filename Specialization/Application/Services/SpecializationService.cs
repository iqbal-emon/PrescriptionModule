using Specialization.Domain.Repositories.Specialization;
using Specialization.Dtos.RequestDto.SpecializationDto;
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

namespace Specialization.Application.Services
{
    public class SpecializationService
    {
        private readonly ISpecializationQueryRepository _specializationQueryRepository;
        private readonly ISpecializationCommandRepository _specializationCommandRepository;
        private readonly MapperService _mapperService;

        public SpecializationService(
            ISpecializationQueryRepository specializationQueryRepository,
            ISpecializationCommandRepository specializationCommandRepository,
            MapperService mapperService)
        {
            _specializationQueryRepository = specializationQueryRepository;
            _specializationCommandRepository = specializationCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Specialization>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Specialization>>();

            try
            {
                var specialization = await _specializationQueryRepository.GetAll();

                if (specialization == null || !specialization.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, specialization?.Result, specialization?.Message ?? "Failed to retrieve specializations", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, specialization.Result, specialization.Message, StatusResponseMessage.success, specialization.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Specialization.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Specialization>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Specialization>();

            try
            {
                var specialization = await _specializationQueryRepository.GetById(id);

                if (specialization == null || !specialization.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, specialization?.Result, specialization?.Message ?? "Specialization not found", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, specialization.Result, specialization.Message, StatusResponseMessage.success, specialization.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Specialization.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Specialization>>> GetBySpecialityId(int specialityId)
        {
            var response = new Response<List<Entities.EntityClass.Specialization>>();

            try
            {
                var specializations = await _specializationQueryRepository.GetBySpecialityId(specialityId);

                if (specializations == null || !specializations.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, specializations?.Result, specializations?.Message ?? "Failed to retrieve specializations by speciality ID", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, specializations.Result, specializations.Message, StatusResponseMessage.success, specializations.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Specializations by speciality ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(SpecializationInsertRequestDto specialization)
        {
            var response = new Response<int>();

            try
            {
                var specializationEntity = await _mapperService.MapSingle<SpecializationInsertRequestDto, Entities.EntityClass.Specialization>(specialization);
                specializationEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _specializationCommandRepository.Insert(specializationEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the Specialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the Specialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(SpecializationUpdateRequestDto specialization)
        {
            var response = new Response<int>();

            try
            {
                var specializationEntity = await _mapperService.MapSingle<SpecializationUpdateRequestDto, Entities.EntityClass.Specialization>(specialization);
                specializationEntity.UpdatedAt = DateTime.Now;
                specializationEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _specializationCommandRepository.Update(specializationEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the Specialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the Specialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _specializationCommandRepository.Delete(id);
        }
    }
}

