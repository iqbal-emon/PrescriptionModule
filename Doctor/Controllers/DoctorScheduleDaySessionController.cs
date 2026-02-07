using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doctor.Application.Services;
using Doctor.Utility;
using Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto;
using Doctor.Dtos.RequestDto.DoctorScheduleDaySessionDto;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorScheduleDaySessionController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorScheduleDaySessionService _daySessionService;

        public DoctorScheduleDaySessionController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorScheduleDaySessionService daySessionService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _daySessionService = daySessionService;
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("gets-all-doctor-schedule-day-sessions")]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleDaySessionApiResponseDto>>>> GetAllDoctorScheduleDaySessions()
        {
            var apiResponse = new ApiResponse<List<DoctorScheduleDaySessionApiResponseDto>>();
            try
            {
                var daySessionList = await _daySessionService.GetAll();
                var mappedDaySessions = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, DoctorScheduleDaySessionApiResponseDto>(daySessionList.Result);

                if (daySessionList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDaySessions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        [HttpGet("get-doctor-schedule-day-session-by-id")]
        public async Task<ActionResult<ApiResponse<DoctorScheduleDaySessionApiResponseDto>>> GetDoctorScheduleDaySessionById(int daySessionId)
        {
            var apiResponse = new ApiResponse<DoctorScheduleDaySessionApiResponseDto>();
            try
            {
                var daySession = await _daySessionService.GetById(daySessionId);
                var mappedDaySession = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, DoctorScheduleDaySessionApiResponseDto>(daySession.Result);

                if (daySession.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedDaySession;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        [HttpPost("create-doctor-schedule-day-session")]
        public async Task<ActionResult<ApiResponse<int>>> CreateDoctorScheduleDaySession(DoctorScheduleDaySessionInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _daySessionService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_insert_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_inserted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        [HttpPut("update-doctor-schedule-day-session")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateDoctorScheduleDaySession(DoctorScheduleDaySessionUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _daySessionService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_update_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_update_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        [HttpDelete("delete-doctor-schedule-day-session-by-id")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorScheduleDaySession(int daySessionId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _daySessionService.Delete(daySessionId);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_delete_success_message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_deleted_failed_message);
                }
            }
            catch (Exception)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, DoctorScheduleDaySessionApiConstantsResponseMessage.daysession_see_try_catch);
            }
            return Ok(apiResponse);
        }

        // ========== Merged from DoctorScheduleDaySessionMainApiController - Backward Compatibility Routes ==========

        [HttpGet("doctor-schedule-day-session/session-list")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleDaySessionApiResponseDto>>>> GetSessionListMainApi()
        {
            // Redirect to GetAllDoctorScheduleDaySessions method
            return await GetAllDoctorScheduleDaySessions();
        }
    }
}

