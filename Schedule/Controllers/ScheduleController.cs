using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Services;
using SharedService.CommonService;
using SharedService.MapService;
using System.Runtime.InteropServices;
using Utility.ApiResponse;
using Utility.Permission;
using Schedule.Dtos.ResponseDto.ScheduleDto;
using Schedule.Utility;
using Schedule.Dtos.RequestDto.Schedule;
using Schedule.Dtos.RequestDto.ScheduleDto;
namespace Schedule.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class ScheduleController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly ScheduleService _scheduleService;

        public ScheduleController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            ScheduleService scheduleService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _scheduleService = scheduleService;
        }

        [Authorize(Policy = PermissionConstants.SchedulesGetAll)]
        [HttpGet("gets-all-schedules")]
        public async Task<ActionResult<ApiResponse<List<ScheduleApiResponseDto>>>> GetAllSchedules()
        {
            var apiResponse = new ApiResponse<List<ScheduleApiResponseDto>>();
            try
            {
                var schedules = await _scheduleService.GetAll();
                var mappedSchedules = await _mapperService.MapList<Entities.EntityClass.Schedule, ScheduleApiResponseDto>(schedules.Result);

                if (schedules.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ScheduleApiConstantsResponseMessage.schedules_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSchedules;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ScheduleApiConstantsResponseMessage.schedules_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ScheduleApiConstantsResponseMessage.schedules_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SchedulesGetId)]
        [HttpGet("get-schedule-by-id")]
        public async Task<ActionResult<ApiResponse<ScheduleApiResponseDto>>> GetScheduleById(int scheduleId)
        {
            var apiResponse = new ApiResponse<ScheduleApiResponseDto>();
            try
            {
                var schedule = await _scheduleService.GetById(scheduleId);
                var mappedSchedule = await _mapperService.MapSingle<Entities.EntityClass.Schedule, ScheduleApiResponseDto>(schedule.Result);

                if (schedule.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, ScheduleApiConstantsResponseMessage.schedules_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSchedule;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, ScheduleApiConstantsResponseMessage.schedules_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, ScheduleApiConstantsResponseMessage.schedules_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SchedulesCreate)]
        [HttpPost("create-schedule")]
        public async Task<ActionResult<ApiResponse<int>>> CreateSchedule(ScheduleInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _scheduleService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ScheduleApiConstantsResponseMessage.schedules_insert_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScheduleApiConstantsResponseMessage.schedules_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScheduleApiConstantsResponseMessage.schedules_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SchedulesUpdate)]
        [HttpPut("update-schedule")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateSchedule(ScheduleUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _scheduleService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ScheduleApiConstantsResponseMessage.schedules_update_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScheduleApiConstantsResponseMessage.schedules_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, ScheduleApiConstantsResponseMessage.schedules_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.SchedulesDelete)]
        [HttpDelete("delete-schedule")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteSchedule(int scheduleId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _scheduleService.Delete(scheduleId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, ScheduleApiConstantsResponseMessage.schedules_delete_success_message);
                    return Ok(apiResponse);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, ScheduleApiConstantsResponseMessage.schedules_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}