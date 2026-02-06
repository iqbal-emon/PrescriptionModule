using Doctor.Domain.Repositories.DoctorSchedule;
using Doctor.Dtos.RequestDto.DoctorScheduleDto;
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
    public class DoctorScheduleService
    {
        private readonly IDoctorScheduleQueryRepository _scheduleQueryRepository;
        private readonly IDoctorScheduleCommandRepository _scheduleCommandRepository;
        private readonly MapperService _mapperService;

        public DoctorScheduleService(
            IDoctorScheduleQueryRepository scheduleQueryRepository,
            IDoctorScheduleCommandRepository scheduleCommandRepository,
            MapperService mapperService)
        {
            _scheduleQueryRepository = scheduleQueryRepository;
            _scheduleCommandRepository = scheduleCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>();

            try
            {
                var schedules = await _scheduleQueryRepository.GetAll();

                if (schedules == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.success, schedules.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the DoctorSchedules.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorSchedule>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorSchedule>();

            try
            {
                var schedule = await _scheduleQueryRepository.GetById(id);

                if (schedule == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedule.Result, schedule.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedule.Result, schedule.Message, StatusResponseMessage.success, schedule.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorScheduleInsertRequestDto schedule)
        {
            var response = new Response<int>();

            try
            {
                var scheduleEntity = await _mapperService.MapSingle<DoctorScheduleInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorSchedule>(schedule);
                scheduleEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _scheduleCommandRepository.Insert(scheduleEntity);

                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorScheduleUpdateRequestDto schedule)
        {
            var response = new Response<int>();

            try
            {
                var scheduleEntity = await _mapperService.MapSingle<DoctorScheduleUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorSchedule>(schedule);
                scheduleEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _scheduleCommandRepository.Update(scheduleEntity);

                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _scheduleCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>();

            try
            {
                var schedules = await _scheduleQueryRepository.GetByDoctorId(doctorId);

                if (schedules == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.success, schedules.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the DoctorSchedules by doctor ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}

