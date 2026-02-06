using Doctor.Domain.Repositories.MasterDoctor;
using Doctor.Dtos.RequestDto.MasterDoctorDto;
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
    public class MasterDoctorService
    {
        private readonly IMasterDoctorQueryRepository _masterDoctorQueryRepository;
        private readonly IMasterDoctorCommandRepository _masterDoctorCommandRepository;
        private readonly MapperService _mapperService;

        public MasterDoctorService(
            IMasterDoctorQueryRepository masterDoctorQueryRepository,
            IMasterDoctorCommandRepository masterDoctorCommandRepository,
            MapperService mapperService)
        {
            _masterDoctorQueryRepository = masterDoctorQueryRepository;
            _masterDoctorCommandRepository = masterDoctorCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.MasterDoctor>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.MasterDoctor>>();

            try
            {
                var masterDoctors = await _masterDoctorQueryRepository.GetAll();

                if (masterDoctors == null)
                {
                    ResponseHelper.SetFailedResponse(response, masterDoctors.Result, masterDoctors.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, masterDoctors.Result, masterDoctors.Message, StatusResponseMessage.success, masterDoctors.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the MasterDoctors.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.MasterDoctor>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.MasterDoctor>();

            try
            {
                var masterDoctor = await _masterDoctorQueryRepository.GetById(id);

                if (masterDoctor == null)
                {
                    ResponseHelper.SetFailedResponse(response, masterDoctor.Result, masterDoctor.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, masterDoctor.Result, masterDoctor.Message, StatusResponseMessage.success, masterDoctor.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the MasterDoctor.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(MasterDoctorInsertRequestDto masterDoctor)
        {
            var response = new Response<int>();

            try
            {
                var masterDoctorEntity = await _mapperService.MapSingle<MasterDoctorInsertRequestDto, Entities.EntityClass.DoctorEntity.MasterDoctor>(masterDoctor);
                masterDoctorEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _masterDoctorCommandRepository.Insert(masterDoctorEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the MasterDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the MasterDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(MasterDoctorUpdateRequestDto masterDoctor)
        {
            var response = new Response<int>();

            try
            {
                var masterDoctorEntity = await _mapperService.MapSingle<MasterDoctorUpdateRequestDto, Entities.EntityClass.DoctorEntity.MasterDoctor>(masterDoctor);
                masterDoctorEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _masterDoctorCommandRepository.Update(masterDoctorEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the MasterDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the MasterDoctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _masterDoctorCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.MasterDoctor>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.MasterDoctor>>();

            try
            {
                var masterDoctors = await _masterDoctorQueryRepository.GetByDoctorId(doctorId);

                if (masterDoctors == null)
                {
                    ResponseHelper.SetFailedResponse(response, masterDoctors.Result, masterDoctors.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, masterDoctors.Result, masterDoctors.Message, StatusResponseMessage.success, masterDoctors.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the MasterDoctors by doctor ID.";
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

