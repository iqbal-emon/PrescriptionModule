using Doctor.Domain.Repositories.Doctor;
using Doctor.Dtos.RequestDto.DoctorDto;
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
    public class DoctorService
    {
        private readonly IDoctorQueryRepository _doctorQueryRepository;
        private readonly IDoctorCommandRepository _doctorCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorService(IDoctorQueryRepository doctorQueryRepository, IDoctorCommandRepository doctorCommandRepository,
            MapperService mapperService)
        {
            _doctorQueryRepository = doctorQueryRepository;
            _doctorCommandRepository = doctorCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Doctor>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Doctor>>();

            try
            {
                var doctors = await _doctorQueryRepository.GetAll();

                if (doctors == null)
                {
                    ResponseHelper.SetFailedResponse(response, doctors.Result, doctors.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, doctors.Result, doctors.Message, StatusResponseMessage.success, doctors.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the doctors.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Doctor>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Doctor>();

            try
            {
                var doctor = await _doctorQueryRepository.GetById(id);

                if (doctor == null)
                {
                    ResponseHelper.SetFailedResponse(response, doctor.Result, doctor.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, doctor.Result, doctor.Message, StatusResponseMessage.success, doctor.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the doctor.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Doctor>> GetByReferenceId(int id)
        {
            var response = new Response<Entities.EntityClass.Doctor>();

            try
            {
                var doctor = await _doctorQueryRepository.GetByReferenceId(id);

                if (doctor == null)
                {
                    ResponseHelper.SetFailedResponse(response, doctor.Result, doctor.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, doctor.Result, doctor.Message, StatusResponseMessage.success, doctor.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the doctor.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }



        

        public async Task<Response<int>> Insert(DoctorInsertRequestDto doctorDto)
        {
            var response = new Response<int>();

            try
            {
                var doctorEntity = await _mapperService.MapSingle<DoctorInsertRequestDto, Entities.EntityClass.Doctor>(doctorDto);
                var insertResponse = await _doctorCommandRepository.Insert(doctorEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the doctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the doctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorUpdateRequestDto doctorDto)
        {
            var response = new Response<int>();

            try
            {
                var doctorEntity = await _mapperService.MapSingle<DoctorUpdateRequestDto, Entities.EntityClass.Doctor>(doctorDto);
                doctorEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _doctorCommandRepository.Update(doctorEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the doctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the doctor.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _doctorCommandRepository.Delete(id);
        }

       
    }
}
