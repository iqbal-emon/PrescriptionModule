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
using Doctor.Utility;
using System.Runtime.InteropServices;
using Doctor.Application.Services;
using Doctor.Dtos.ResponseDto.DoctorScheduleDto;
using Doctor.Dtos.RequestDto.DoctorScheduleDto;
using Newtonsoft.Json;
using System.Linq;

namespace Doctor.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly DoctorScheduleService _scheduleService;
        private readonly DoctorScheduleDaySessionService _daySessionService;

        public DoctorScheduleController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            DoctorScheduleService scheduleService,
            DoctorScheduleDaySessionService daySessionService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _scheduleService = scheduleService;
            _daySessionService = daySessionService;
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

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-doctor-schedule-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleApiResponseDto>>>> GetDoctorScheduleListByDoctorId([FromQuery] int doctorId, [FromQuery] int? scheduleId = null)
        {
            var apiResponse = new ApiResponse<List<DoctorScheduleApiResponseDto>>();
            try
            {
                var scheduleList = await _scheduleService.GetByDoctorId(doctorId);
                var mappedSchedules = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(scheduleList.Result);

                if (scheduleList.Result == null || scheduleList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, DoctorScheduleApiConstantsResponseMessage.schedule_null_of_get_list);
                    return Ok(apiResponse);
                }

                // Filter by scheduleId if provided
                if (scheduleId.HasValue && scheduleId.Value > 0)
                {
                    mappedSchedules = mappedSchedules.Where(s => s.DoctorScheduleID == scheduleId.Value).ToList();
                }

                // Populate sessions for each schedule
                foreach (var schedule in mappedSchedules)
                {
                    var sessionsResponse = await _daySessionService.GetByDoctorScheduleId(schedule.DoctorScheduleID);
                    if (sessionsResponse.IsSuccess && sessionsResponse.Result != null)
                    {
                        var mappedSessions = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(sessionsResponse.Result);
                        schedule.DoctorScheduleDaySession = mappedSessions;
                    }
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

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-doctor-schedule-sessions-list-by-doctor-id")]
        public async Task<ActionResult<ApiResponse<List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>>>> GetDoctorScheduleSessionsListByDoctorId([FromQuery] int doctorId, [FromQuery] int? scheduleId = null)
        {
            var apiResponse = new ApiResponse<List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>>();
            try
            {
                var scheduleList = await _scheduleService.GetByDoctorId(doctorId);
                var mappedSchedules = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(scheduleList.Result);

                if (scheduleList.Result == null || scheduleList.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), DoctorScheduleApiConstantsResponseMessage.schedule_null_of_get_list);
                    return Ok(apiResponse);
                }

                // Filter by scheduleId if provided
                if (scheduleId.HasValue && scheduleId.Value > 0)
                {
                    mappedSchedules = mappedSchedules.Where(s => s.DoctorScheduleID == scheduleId.Value).ToList();
                }

                // Collect all sessions from all schedules into a flat list
                var allSessions = new List<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>();

                foreach (var schedule in mappedSchedules)
                {
                    var sessionsResponse = await _daySessionService.GetByDoctorScheduleId(schedule.DoctorScheduleID);
                    if (sessionsResponse.IsSuccess && sessionsResponse.Result != null)
                    {
                        allSessions.AddRange(sessionsResponse.Result);
                    }
                }

                // Map all sessions to response DTO
                var mappedSessions = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(allSessions);

                apiResponse.Results = mappedSessions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Sessions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), $"Error: {ex.Message}", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        [HttpGet("get-doctor-schedule-sessions-by-chamber-id")]
        public async Task<ActionResult<ApiResponse<List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>>>> GetSessionsByDoctorChamberId([FromQuery] int doctorId, [FromQuery] int doctorChamberId)
        {
            var apiResponse = new ApiResponse<List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>>();
            try
            {
                // Get all schedules for the doctor and chamber
                var schedules = await _scheduleService.GetByDoctorIdAndChamberId(doctorId, doctorChamberId);
                if (schedules.Result == null || schedules.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), "No schedules found for this chamber", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                    return Ok(apiResponse);
                }

                // Collect all sessions from all schedules for this chamber
                var allSessions = new List<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>();

                foreach (var schedule in schedules.Result)
                {
                    var sessionsResponse = await _daySessionService.GetByDoctorScheduleId(schedule.DoctorScheduleID);
                    if (sessionsResponse.IsSuccess && sessionsResponse.Result != null)
                    {
                        allSessions.AddRange(sessionsResponse.Result);
                    }
                }

                // Map all sessions to response DTO
                var mappedSessions = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(allSessions);

                apiResponse.Results = mappedSessions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Sessions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), $"Error: {ex.Message}", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return Ok(apiResponse);
        }

        // ========== Merged from DoctorScheduleMainApiController - Backward Compatibility Routes ==========

        [HttpPost("doctor-schedule")]
        [Authorize(Policy = PermissionConstants.DegreeCreate)]
        public async Task<ActionResult<ApiResponse<DoctorScheduleApiResponseDto>>> CreateDoctorScheduleMainApi([FromBody] DoctorScheduleInputRequestDto request)
        {
            var apiResponse = new ApiResponse<DoctorScheduleApiResponseDto>();
            try
            {
                // Check if it's the comprehensive DTO (has DoctorScheduleDaySession array)
                if (request != null && request.DoctorScheduleDaySession != null && request.DoctorScheduleDaySession.Count > 0)
                {
                    // Use the comprehensive creation method
                    var response = await _scheduleService.InsertCompleteSchedule(request);
                    
                    if (response.IsSuccess)
                    {
                        // Get the created schedule to return full details
                        var createdSchedule = await _scheduleService.GetById(response.Result);
                        if (createdSchedule.IsSuccess && createdSchedule.Result != null)
                        {
                            var mappedSchedule = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(createdSchedule.Result);
                            apiResponse.Results = mappedSchedule;
                            ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, response.Message ?? DoctorScheduleApiConstantsResponseMessage.schedule_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                        }
                        else
                        {
                            ApiResponseHelper.SetFailedResponse(apiResponse, null, "Schedule created but could not retrieve details.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
                        }
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, response.Message ?? DoctorScheduleApiConstantsResponseMessage.schedule_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    }
                }
                else
                {
                    // If no sessions provided, try to convert to old format for backward compatibility
                    if (request != null && request.DoctorProfileId > 0)
                    {
                        var simpleDto = new DoctorScheduleInsertRequestDto
                        {
                            DoctorID = request.DoctorProfileId,
                            ScheduleID = 0,
                            TenantID = request.TenantID > 0 ? request.TenantID : 0
                        };
                        
                        var createResponse = await CreateDoctorSchedule(simpleDto);
                        // Convert the int response to DoctorScheduleApiResponseDto for consistency
                        if (createResponse.Result is OkObjectResult okResult && okResult.Value is ApiResponse<int> intResponse)
                        {
                            if (intResponse.Results > 0)
                            {
                                var createdSchedule = await _scheduleService.GetById(intResponse.Results);
                                if (createdSchedule.IsSuccess && createdSchedule.Result != null)
                                {
                                    var mappedSchedule = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(createdSchedule.Result);
                                    apiResponse.Results = mappedSchedule;
                                    ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, intResponse.Message, StatusResponseMessage.success, StatusCodes.Status201Created);
                                }
                                else
                                {
                                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Schedule created but could not retrieve details.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
                                }
                            }
                            else
                            {
                                ApiResponseHelper.SetFailedResponse(apiResponse, null, intResponse.Message ?? "Failed to create schedule.", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                            }
                        }
                        else
                        {
                            ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid response from schedule creation.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
                        }
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid request format. DoctorProfileId and at least one session are required.", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            
            return Ok(apiResponse);
        }

        [HttpGet("doctor-schedule/{id}")]
        [Authorize(Policy = PermissionConstants.DegreeGetId)]
        public async Task<ActionResult<ApiResponse<DoctorScheduleApiResponseDto>>> GetDoctorScheduleByIdMainApi(int id)
        {
            return await GetDoctorScheduleById(id);
        }

        [HttpGet("doctor-schedule/by-doctor-id-list/{doctorId}")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleApiResponseDto>>>> GetSchedulesByDoctorIdMainApi(int doctorId)
        {
            return await GetDoctorScheduleListByDoctorId(doctorId, null);
        }

        [HttpGet("doctor-schedule/sessions-by-doctor-schedule-id")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>>>> GetSessionsByDoctorScheduleId([FromQuery] int doctorId, [FromQuery] int scheduleId)
        {
            var apiResponse = new ApiResponse<List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>>();
            try
            {
                // Verify the schedule belongs to the doctor
                var schedule = await _scheduleService.GetById(scheduleId);
                if (!schedule.IsSuccess || schedule.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), "Schedule not found", StatusResponseMessage.failed, StatusCodes.Status404NotFound);
                    return Ok(apiResponse);
                }

                // Verify the schedule belongs to the specified doctor
                if (schedule.Result.DoctorID != doctorId)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), "Schedule does not belong to the specified doctor", StatusResponseMessage.failed, StatusCodes.Status403Forbidden);
                    return Ok(apiResponse);
                }

                // Fetch sessions for the schedule
                var sessionsResponse = await _daySessionService.GetByDoctorScheduleId(scheduleId);
                if (!sessionsResponse.IsSuccess || sessionsResponse.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), "Failed to retrieve sessions", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    return Ok(apiResponse);
                }

                // Map sessions to response DTO
                var mappedSessions = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(sessionsResponse.Result);

                apiResponse.Results = mappedSessions;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Sessions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(), $"Error: {ex.Message}", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            return Ok(apiResponse);
        }

        [HttpGet("doctor-schedule/details-schedule-list-by-doctor-chamber-id")]
        [Authorize(Policy = PermissionConstants.DegreeGetAll)]
        public async Task<ActionResult<ApiResponse<List<DoctorScheduleApiResponseDto>>>> GetDetailsScheduleListByDoctorChamberIdMainApi([FromQuery] int doctorId, [FromQuery] int chamberId)
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
                
                // Populate sessions for each schedule
                foreach (var schedule in mappedSchedules)
                {
                    var sessionsResponse = await _daySessionService.GetByDoctorScheduleId(schedule.DoctorScheduleID);
                    if (sessionsResponse.IsSuccess && sessionsResponse.Result != null)
                    {
                        var mappedSessions = await _mapperService.MapList<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto.DoctorScheduleDaySessionApiResponseDto>(sessionsResponse.Result);
                        schedule.DoctorScheduleDaySession = mappedSessions;
                    }
                }
                
                apiResponse.Results = mappedSchedules;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Schedules retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DoctorScheduleApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut("doctor-schedule")]
        [Authorize(Policy = PermissionConstants.DegreeUpdate)]
        public async Task<ActionResult<ApiResponse<DoctorScheduleApiResponseDto>>> UpdateDoctorScheduleMainApi([FromBody] DoctorScheduleInputRequestDto request)
        {
            var apiResponse = new ApiResponse<DoctorScheduleApiResponseDto>();
            try
            {
                // Check if it's the comprehensive DTO (has DoctorScheduleDaySession array and Id)
                if (request != null && request.Id.HasValue && request.DoctorScheduleDaySession != null && request.DoctorScheduleDaySession.Count > 0)
                {
                    // Use the comprehensive update method
                    var response = await _scheduleService.UpdateCompleteSchedule(request);
                    
                    if (response.IsSuccess)
                    {
                        // Get the updated schedule to return full details
                        var updatedSchedule = await _scheduleService.GetById(response.Result);
                        if (updatedSchedule.IsSuccess && updatedSchedule.Result != null)
                        {
                            var mappedSchedule = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(updatedSchedule.Result);
                            apiResponse.Results = mappedSchedule;
                            ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, response.Message ?? DoctorScheduleApiConstantsResponseMessage.schedule_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
                        }
                        else
                        {
                            ApiResponseHelper.SetFailedResponse(apiResponse, null, "Schedule updated but could not retrieve details.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
                        }
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, response.Message ?? DoctorScheduleApiConstantsResponseMessage.schedule_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    }
                }
                else
                {
                    // If no sessions provided or no Id, try to convert to old format for backward compatibility
                    if (request != null && request.Id.HasValue && request.DoctorProfileId > 0)
                    {
                        var simpleDto = new DoctorScheduleUpdateRequestDto
                        {
                            DoctorScheduleID = request.Id.Value,
                            DoctorID = request.DoctorProfileId,
                            ScheduleID = 0,
                            TenantID = request.TenantID > 0 ? request.TenantID : 0
                        };
                        
                        var updateResponse = await UpdateDoctorSchedule(simpleDto);
                        // Convert the int response to DoctorScheduleApiResponseDto for consistency
                        if (updateResponse.Result is OkObjectResult okResult && okResult.Value is ApiResponse<int> intResponse)
                        {
                            if (intResponse.Results > 0)
                            {
                                var updatedSchedule = await _scheduleService.GetById(intResponse.Results);
                                if (updatedSchedule.IsSuccess && updatedSchedule.Result != null)
                                {
                                    var mappedSchedule = await _mapperService.MapSingle<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleApiResponseDto>(updatedSchedule.Result);
                                    apiResponse.Results = mappedSchedule;
                                    ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, intResponse.Message, StatusResponseMessage.success, StatusCodes.Status200OK);
                                }
                                else
                                {
                                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Schedule updated but could not retrieve details.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
                                }
                            }
                            else
                            {
                                ApiResponseHelper.SetFailedResponse(apiResponse, null, intResponse.Message ?? "Failed to update schedule.", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                            }
                        }
                        else
                        {
                            ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid response from schedule update.", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
                        }
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, null, "Invalid request format. Id, DoctorProfileId and at least one session are required.", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}", StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            
            return Ok(apiResponse);
        }

        [HttpDelete("doctor-schedule/{id}")]
        [Authorize(Policy = PermissionConstants.DegreeDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteDoctorScheduleMainApi(int id)
        {
            return await DeleteDoctorSchedule(id);
        }
    }
}

