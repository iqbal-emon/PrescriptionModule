using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorChamber.Utility;
using System.Runtime.InteropServices;
using DoctorSchedule.Application.Services;
using DoctorSchedule.Dtos.ResponseDto.DoctorScheduleDto;
using DoctorSchedule.Dtos.RequestDto.DoctorScheduleDto;

namespace DoctorSchedule.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorScheduleService _scheduleService;

        public DoctorScheduleController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorScheduleService scheduleService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _scheduleService = scheduleService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-schedules")]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleApiResponseDto>>>> GetAllDoctorSchedules()
        {
            var apiResponse = new ApiResponse<List<DoctorScheduleApiResponseDto>>();
            try
            {
                var scheduleList = await _scheduleService.GetAll();
                var mappedSchedules = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(scheduleList.Result);

                if (scheduleList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleApiConstantsResponseMessage.schedule_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSchedules;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorScheduleApiConstantsResponseMessage.schedule_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleApiConstantsResponseMessage.schedule_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-schedule-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorScheduleApiResponseDto>>> GetDoctorScheduleById(int scheduleId)
        {
            var apiResponse = new ApiResponse<DoctorScheduleApiResponseDto>();
            try
            {
                var schedule = await _scheduleService.GetById(scheduleId);
                var mappedSchedule = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(schedule.Result);

                if (schedule.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleApiConstantsResponseMessage.schedule_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedSchedule;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorScheduleApiConstantsResponseMessage.schedule_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleApiConstantsResponseMessage.schedule_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-schedule")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorSchedule(DoctorScheduleInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _scheduleService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduleApiConstantsResponseMessage.schedule_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleApiConstantsResponseMessage.schedule_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleApiConstantsResponseMessage.schedule_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-schedule")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorSchedule(DoctorScheduleUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _scheduleService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduleApiConstantsResponseMessage.schedule_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleApiConstantsResponseMessage.schedule_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleApiConstantsResponseMessage.schedule_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-schedule-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorSchedule(int scheduleId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _scheduleService.Delete(scheduleId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduleApiConstantsResponseMessage.schedule_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorScheduleApiConstantsResponseMessage.schedule_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorScheduleApiConstantsResponseMessage.schedule_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}