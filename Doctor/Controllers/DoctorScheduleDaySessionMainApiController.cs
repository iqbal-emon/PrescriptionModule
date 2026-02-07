using Doctor.Application.Services;
using Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto;
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
    [Route("api/2025-02/doctor-schedule-day-session")]
    public class DoctorScheduleDaySessionMainApiController : ControllerBase
    {
        private readonly DoctorScheduleDaySessionService _daySessionService;
        private readonly MapperService _mapperService;

        public DoctorScheduleDaySessionMainApiController(
            DoctorScheduleDaySessionService daySessionService,
            MapperService mapperService)
        {
            _daySessionService = daySessionService;
            _mapperService = mapperService;
        }

        [HttpGet("session-list")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleDaySessionApiResponseDto>>>> GetSessionList()
        {
            var apiResponse = new ApiResponse<List<DoctorScheduleDaySessionApiResponseDto>>();
            try
            {
                var daySessionList = await _daySessionService.GetAll();
                var mappedDaySessions = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, DoctorScheduleDaySessionApiResponseDto>(daySessionList.Result);

                if (daySessionList.Result == null || daySessionList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorScheduleDaySessionApiResponseDto>(), "No sessions found");
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDaySessions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Sessions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorScheduleDaySessionApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }
}

