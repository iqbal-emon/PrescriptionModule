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

        public async Task<Response<Entities.EntityClass.Doctor>> GetByUserName(string userName)
        {
            var response = new Response<Entities.EntityClass.Doctor>();

            try
            {
                var doctor = await _doctorQueryRepository.GetByUserName(userName);

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
                response.Message = "A database error occurred while retrieving the doctor by username.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Doctor>> GetByEmail(string email)
        {
            var response = new Response<Entities.EntityClass.Doctor>();

            try
            {
                var doctor = await _doctorQueryRepository.GetByEmail(email);

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
                response.Message = "A database error occurred while retrieving the doctor by email.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Doctor>>> GetByOnlineStatus(bool isOnline)
        {
            var response = new Response<List<Entities.EntityClass.Doctor>>();

            try
            {
                var doctors = await _doctorQueryRepository.GetByOnlineStatus(isOnline);

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
                response.Message = "A database error occurred while retrieving doctors by online status.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Doctor>>> GetByActiveStatus(bool isActive)
        {
            var response = new Response<List<Entities.EntityClass.Doctor>>();

            try
            {
                var doctors = await _doctorQueryRepository.GetByActiveStatus(isActive);

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
                response.Message = "A database error occurred while retrieving doctors by active status.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<bool>> UpdateActiveStatus(int doctorId, bool isActive)
        {
            var response = new Response<bool>();

            try
            {
                var result = await _doctorCommandRepository.UpdateActiveStatus(doctorId, isActive);
                response = result;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the doctor's active status.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the doctor's active status.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<bool>> UpdateOnlineStatus(int doctorId, bool isOnline)
        {
            var response = new Response<bool>();

            try
            {
                var result = await _doctorCommandRepository.UpdateOnlineStatus(doctorId, isOnline);
                response = result;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the doctor's online status.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the doctor's online status.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<bool>> UpdateExpertise(int doctorId, string expertise)
        {
            var response = new Response<bool>();

            try
            {
                var result = await _doctorCommandRepository.UpdateExpertise(doctorId, expertise);
                response = result;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the doctor's expertise.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the doctor's expertise.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<bool>> UpdateProfileStep(int doctorId, int profileStep)
        {
            var response = new Response<bool>();

            try
            {
                var result = await _doctorCommandRepository.UpdateProfileStep(doctorId, profileStep);
                response = result;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the doctor's profile step.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the doctor's profile step.";
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.Doctor>>> GetByCreatorId(int creatorId)
        {
            var response = new Response<List<Entities.EntityClass.Doctor>>();

            try
            {
                var doctors = await _doctorQueryRepository.GetByCreatorId(creatorId);

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
                response.Message = "A database error occurred while retrieving doctors by creator ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Doctor>> GetDetailsByAdmin(int doctorId)
        {
            var response = new Response<Entities.EntityClass.Doctor>();

            try
            {
                var doctor = await _doctorQueryRepository.GetDetailsByAdmin(doctorId);

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
                response.Message = "A database error occurred while retrieving doctor details by admin.";
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
