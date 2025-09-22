using Microsoft.AspNetCore.Http;
using Schedule.Domain.Repositories;
using Schedule.Domain.Repositories.Schedule;
using Schedule.Dtos.RequestDto;
using Schedule.Dtos.RequestDto.Schedule;
using Schedule.Dtos.RequestDto.ScheduleDto;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace Schedule.Application.Services
{
    public class ScheduleService
    {
        private readonly IScheduleQueryRepository _scheduleQueryRepository;
        private readonly IScheduleCommandRepository _scheduleCommandRepository;
        private readonly MapperService _mapperService;

        public ScheduleService(IScheduleQueryRepository scheduleQueryRepository, IScheduleCommandRepository scheduleCommandRepository,
            MapperService mapperService)
        {
            _scheduleQueryRepository = scheduleQueryRepository;
            _scheduleCommandRepository = scheduleCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.Schedule>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Schedule>>();

            try
            {
                var schedules = await _scheduleQueryRepository.GetAll();

                if (schedules == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.success, schedules.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the schedules.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Schedule>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Schedule>();

            try
            {
                var schedule = await _scheduleQueryRepository.GetById(id);

                if (schedule == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedule.Result, schedule.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedule.Result, schedule.Message, StatusResponseMessage.success, schedule.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the schedule.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(ScheduleInsertRequestDto schedule)
        {
            var response = new Response<int>();

            try
            {
                var scheduleEntity = await _mapperService.MapSingle<ScheduleInsertRequestDto, Entities.EntityClass.Schedule>(schedule);
                var insertResponse = await _scheduleCommandRepository.Insert(scheduleEntity);
                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the schedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the schedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(ScheduleUpdateRequestDto schedule)
        {
            var response = new Response<int>();

            try
            {
                var scheduleEntity = await _mapperService.MapSingle<ScheduleUpdateRequestDto, Entities.EntityClass.Schedule>(schedule);
                scheduleEntity.UpdatedAt = DateTime.Now;
                scheduleEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _scheduleCommandRepository.Update(scheduleEntity);
                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the schedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the schedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _scheduleCommandRepository.Delete(id);
        }
    }
}
