using Appointment.Application.Services;
using Appointment.Dtos.ResponseDto.AppointmentDto;
using Doctor.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace Appointment.Controllers
{
    [ApiController]
    [Route("api/app/appointment")]
    public class AppointmentMainApiController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly DoctorScheduleDaySessionService _daySessionService;
        private readonly MapperService _mapperService;

        public AppointmentMainApiController(AppointmentService appointmentService, DoctorScheduleDaySessionService daySessionService, MapperService mapperService)
        {
            _appointmentService = appointmentService;
            _daySessionService = daySessionService;
            _mapperService = mapperService;
        }

        [HttpGet("patient-list-by-doctor-id/{doctorId}")]
        [Authorize(Policy = PermissionConstants.AppointmentGetAll)]
        public async Task<ActionResult<ApiResponse<List<PatientListByDoctorDto>>>> GetPatientListByDoctorId(int doctorId)
        {
            var apiResponse = new ApiResponse<List<PatientListByDoctorDto>>();
            try
            {
                // Get appointments by doctor ID
                var appointmentsResponse = await _appointmentService.GetAll(doctorId, 1, 1000, null, null, null);
                
                if (appointmentsResponse?.Result?.Result == null || !appointmentsResponse.Result.Result.Any())
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientListByDoctorDto>(), "No patients found for this doctor");
                    return Ok(apiResponse);
                }

                // Extract unique patients from appointments
                var uniquePatients = appointmentsResponse.Result.Result
                    .Where(a => a.PatientId.HasValue)
                    .GroupBy(a => a.PatientId.Value)
                    .Select(g => g.First())
                    .Select(a => new PatientListByDoctorDto
                    {
                        PatientId = a.PatientId ?? 0,
                        PatientName = a.PatientName ?? "N/A",
                        PatientCode = a.PatientCode ?? "N/A",
                        PatientMobileNo = a.PatientMobileNo ?? "N/A",
                        PatientEmail = a.PatientEmail ?? "N/A"
                    })
                    .ToList();

                ApiResponseHelper.SetSuccessResponse(apiResponse, uniquePatients, "Patients retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<PatientListByDoctorDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        // Note: session-list endpoint moved to DoctorScheduleDaySessionMainApiController
        // This endpoint is kept for backward compatibility but should use /api/app/doctor-schedule-day-session/session-list
        [HttpGet("session-list")]
        [Authorize(Policy = PermissionConstants.AppointmentGetAll)]
        [Obsolete("Use /api/app/doctor-schedule-day-session/session-list instead")]
        public async Task<ActionResult<ApiResponse<List<SessionListDto>>>> GetSessionList()
        {
            var apiResponse = new ApiResponse<List<SessionListDto>>();
            try
            {
                var daySessions = await _daySessionService.GetAll();
                if (daySessions.Result == null || daySessions.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<SessionListDto>(), "No sessions found");
                    return Ok(apiResponse);
                }

                var sessionList = daySessions.Result.Select(s => new SessionListDto
                {
                    SessionId = s.DoctorScheduleDaySessionID,
                    SessionName = $"{s.ScheduleDayofWeek} - {s.StartTime} to {s.EndTime}",
                    StartTime = ParseTime(s.StartTime),
                    EndTime = ParseTime(s.EndTime)
                }).ToList();

                ApiResponseHelper.SetSuccessResponse(apiResponse, sessionList, "Sessions retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<SessionListDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        private DateTime? ParseTime(string timeString)
        {
            if (string.IsNullOrWhiteSpace(timeString))
                return null;

            if (TimeSpan.TryParse(timeString, out var timeSpan))
            {
                return DateTime.Today.Add(timeSpan);
            }
            return null;
        }
    }

    // DTO for patient list
    public class PatientListByDoctorDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public string PatientMobileNo { get; set; }
        public string PatientEmail { get; set; }
    }

    // DTO for session list
    public class SessionListDto
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}

