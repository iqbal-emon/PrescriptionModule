using Doctor.Application.Services;
using Doctor.Dtos.RequestDto.DoctorScheduleDto;
using Doctor.Dtos.ResponseDto.DoctorScheduleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/doctor-schedule")]
    public class DoctorScheduleMainApiController : ControllerBase
    {
        private readonly DoctorScheduleService _scheduleService;
        private readonly MapperService _mapperService;

        public DoctorScheduleMainApiController(DoctorScheduleService scheduleService, MapperService mapperService)
        {
            _scheduleService = scheduleService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorSchedule([FromBody] DoctorScheduleInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _scheduleService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor schedule created successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<DoctorScheduleApiResponseDto>>> GetDoctorScheduleById(int id)
        {
            var apiResponse = new ApiResponse<DoctorScheduleApiResponseDto>();
            try
            {
                var schedule = await _scheduleService.GetById(id);
                if (schedule.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Schedule not found");
                    return Ok(apiResponse);
                }

                var mappedSchedule = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(schedule.Result);
                apiResponse.Results = mappedSchedule;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Schedule retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("by-doctor-id-list/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleApiResponseDto>>>> GetSchedulesByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<DoctorScheduleApiResponseDto>>();
            try
            {
                var schedules = await _scheduleService.GetByDoctorId(doctorId);
                if (schedules.Result == null || schedules.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorScheduleApiResponseDto>(), "No schedules found");
                    return Ok(apiResponse);
                }

                var mappedSchedules = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(schedules.Result);
                apiResponse.Results = mappedSchedules;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Schedules retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorScheduleApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("details-schedule-list-by-doctor-chamber-id")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleApiResponseDto>>>> GetDetailsScheduleListByDoctorChamberId([FromQuery] int doctorId, [FromQuery] int chamberId)
        {
            var apiResponse = new ApiResponse<List<DoctorScheduleApiResponseDto>>();
            try
            {
                var schedules = await _scheduleService.GetByDoctorIdAndChamberId(doctorId, chamberId);
                if (schedules.Result == null || schedules.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorScheduleApiResponseDto>(), "No schedules found");
                    return Ok(apiResponse);
                }

                var mappedSchedules = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(schedules.Result);
                apiResponse.Results = mappedSchedules;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Schedules retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorScheduleApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorSchedule([FromBody] DoctorScheduleUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _scheduleService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor schedule updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorSchedule(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _scheduleService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Doctor schedule deleted successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }
}

