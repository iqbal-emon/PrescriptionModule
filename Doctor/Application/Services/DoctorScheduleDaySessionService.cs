using Doctor.Domain.Repositories.DoctorScheduleDaySession;
using Doctor.Dtos.RequestDto.DoctorScheduleDaySessionDto;
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
    public class DoctorScheduleDaySessionService
    {
        private readonly IDoctorScheduleDaySessionQueryRepository _daySessionQueryRepository;
        private readonly IDoctorScheduleDaySessionCommandRepository _daySessionCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorScheduleDaySessionService(
            IDoctorScheduleDaySessionQueryRepository daySessionQueryRepository,
            IDoctorScheduleDaySessionCommandRepository daySessionCommandRepository,
            MapperService mapperService)
        {
            _daySessionQueryRepository = daySessionQueryRepository;
            _daySessionCommandRepository = daySessionCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>>();

            try
            {
                var daySessions = await _daySessionQueryRepository.GetAll();

                if (daySessions == null)
                {
                    ResponseHelper.SetFailedResponse(response, daySessions.Result, daySessions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, daySessions.Result, daySessions.Message, StatusResponseMessage.success, daySessions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorScheduleDaySessions.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>();

            try
            {
                var daySession = await _daySessionQueryRepository.GetById(id);

                if (daySession == null)
                {
                    ResponseHelper.SetFailedResponse(response, daySession.Result, daySession.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, daySession.Result, daySession.Message, StatusResponseMessage.success, daySession.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorScheduleDaySession.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorScheduleDaySessionInsertRequestDto daySession)
        {
            var response = new Response<int>();

            try
            {
                var daySessionEntity = await _mapperService.MapSingle<DoctorScheduleDaySessionInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>(daySession);
                daySessionEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _daySessionCommandRepository.Insert(daySessionEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the DoctorScheduleDaySession.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorScheduleDaySession.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorScheduleDaySessionUpdateRequestDto daySession)
        {
            var response = new Response<int>();

            try
            {
                var daySessionEntity = await _mapperService.MapSingle<DoctorScheduleDaySessionUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>(daySession);
                daySessionEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _daySessionCommandRepository.Update(daySessionEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the DoctorScheduleDaySession.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the DoctorScheduleDaySession.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _daySessionCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>>> GetByDoctorScheduleId(int doctorScheduleId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>>();

            try
            {
                var daySessions = await _daySessionQueryRepository.GetByDoctorScheduleId(doctorScheduleId);

                if (daySessions == null)
                {
                    ResponseHelper.SetFailedResponse(response, daySessions.Result, daySessions.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, daySessions.Result, daySessions.Message, StatusResponseMessage.success, daySessions.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the DoctorScheduleDaySessions by schedule ID.";
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

