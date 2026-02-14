using Doctor.Domain.Repositories.DoctorSpecialization;
using Doctor.Dtos.RequestDto.DoctorSpecializationDto;
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
    public class DoctorSpecializationService
    {
        private readonly IDoctorSpecializationQueryRepository _specializationQueryRepository;
        private readonly IDoctorSpecializationCommandRepository _specializationCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorSpecializationService(
            IDoctorSpecializationQueryRepository specializationQueryRepository,
            IDoctorSpecializationCommandRepository specializationCommandRepository,
            MapperService mapperService)
        {
            _specializationQueryRepository = specializationQueryRepository;
            _specializationCommandRepository = specializationCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>();

            try
            {
                var specializations = await _specializationQueryRepository.GetAll();

                if (specializations == null)
                {
                    ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), "Failed to retrieve specializations", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    // Ensure Result is never null - use empty list if null
                    var result = specializations.Result ?? new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>();
                    ResponseHelper.SetSuccessResponse(response, result, specializations.Message, StatusResponseMessage.success, specializations.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorSpecializations.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorSpecialization>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorSpecialization>();

            try
            {
                var specialization = await _specializationQueryRepository.GetById(id);

                if (specialization == null)
                {
                    ResponseHelper.SetFailedResponse(response, specialization.Result, specialization.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, specialization.Result, specialization.Message, StatusResponseMessage.success, specialization.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorSpecialization.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorSpecializationInsertRequestDto specialization)
        {
            var response = new Response<int>();

            try
            {
                var specializationEntity = await _mapperService.MapSingle<DoctorSpecializationInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorSpecialization>(specialization);
                specializationEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _specializationCommandRepository.Insert(specializationEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the DoctorSpecialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorSpecialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorSpecializationUpdateRequestDto specialization)
        {
            var response = new Response<int>();

            try
            {
                var specializationEntity = await _mapperService.MapSingle<DoctorSpecializationUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorSpecialization>(specialization);
                specializationEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _specializationCommandRepository.Update(specializationEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the DoctorSpecialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the DoctorSpecialization.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _specializationCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>();

            try
            {
                var specializations = await _specializationQueryRepository.GetByDoctorId(doctorId);

                if (specializations == null)
                {
                    ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), "Failed to retrieve specializations", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    // Ensure Result is never null - use empty list if null
                    var result = specializations.Result ?? new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>();
                    ResponseHelper.SetSuccessResponse(response, result, specializations.Message, StatusResponseMessage.success, specializations.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorSpecializations by doctor ID.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>> GetBySpecialityId(int specialityId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>();

            try
            {
                var specializations = await _specializationQueryRepository.GetBySpecialityId(specialityId);

                if (specializations == null)
                {
                    ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), "Failed to retrieve specializations", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    // Ensure Result is never null - use empty list if null
                    var result = specializations.Result ?? new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>();
                    ResponseHelper.SetSuccessResponse(response, result, specializations.Message, StatusResponseMessage.success, specializations.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorSpecializations by speciality ID.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>> GetByDoctorIdAndSpecialityId(int doctorId, int specialityId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>();

            try
            {
                var specializations = await _specializationQueryRepository.GetByDoctorIdAndSpecialityId(doctorId, specialityId);

                if (specializations == null)
                {
                    ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), "Failed to retrieve specializations", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    // Ensure Result is never null - use empty list if null
                    var result = specializations.Result ?? new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>();
                    ResponseHelper.SetSuccessResponse(response, result, specializations.Message, StatusResponseMessage.success, specializations.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorSpecializations by doctor and speciality ID.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, new List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>(), response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}

