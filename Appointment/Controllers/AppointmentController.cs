using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using Appointment.Application.Services;
using Appointment.Dtos.RequestDto;
using Appointment.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appointment.Dtos.RequestDto.AppointmentDto;
using Pharmacies.Utility;
using Appointment.Dtos.ResponseDto.AppointmentDto;

namespace Appointment.Controllers
{
    [ApiController]
    [Route("api/2025-20/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly AppointmentService _appointmentService;

        public AppointmentController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            AppointmentService appointmentService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _appointmentService = appointmentService;
        }

        [Authorize(Policy = PermissionConstants.AppointmentGetAll)]
        [HttpGet("appointment-get-by-doctorId")]
        public async Task<ActionResult<ApiResponse<List<AppointmentApiResponseDto>>>> GetAllAppointments(int doctorId)
        {
            var apiResponse = new ApiResponse<List<AppointmentApiResponseDto>>();
            try
            {
                var appointmentsResponse = await _appointmentService.GetAll(doctorId);
                var appointments = appointmentsResponse.Result;

                if (appointments == null || appointments.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "No appointments found.");
                    return Ok(apiResponse);
                }

                // Loop through each appointment to get Session & Schedule details
                foreach (var appointment in appointments)
                {
                    if (appointment.SessionId > 0)
                    {
                        var sessionResponse = await _appointmentService.GetBySessionId(appointment.SessionId);
                        if (sessionResponse.IsSuccess && sessionResponse.Result != null)
                        {
                            appointment.SessionName = sessionResponse.Result.DoctorScheduleName;
                            appointment.ScheduleDayofWeek = sessionResponse.Result.ScheduleDayofWeek;
                            appointment.StartTime = sessionResponse.Result.StartTime;
                            appointment.EndTime = sessionResponse.Result.EndTime;
                            appointment.NoOfPatients = sessionResponse.Result.NoOfPatients;
                            appointment.IsActive = sessionResponse.Result.IsActive;
                        }
                    }

                    if (appointment.ScheduleId > 0)
                    {
                        var scheduleResponse = await _appointmentService.GetBySchedule(appointment.ScheduleId);
                        if (scheduleResponse.IsSuccess && scheduleResponse.Result != null)
                        {
                            appointment.ScheduleName = scheduleResponse.Result.ScheduleName;
                            appointment.ScheduleTypeName = scheduleResponse.Result.ScheduleTypeName;
                            appointment.ConsultancyTypeName = scheduleResponse.Result.ConsultancyTypeName;
                            appointment.DoctorChamberId = scheduleResponse.Result.DoctorChamberId;
                            appointment.Chamber = scheduleResponse.Result.Chamber;
                            appointment.Status = scheduleResponse.Result.Status;
                            appointment.OffDayFrom = scheduleResponse.Result.OffDayFrom;
                            appointment.DayTextFrom = scheduleResponse.Result.DayTextFrom;
                            appointment.OffDayTo = scheduleResponse.Result.OffDayTo;
                            appointment.DayTextTo = scheduleResponse.Result.DayTextTo;
                            appointment.Remarks = scheduleResponse.Result.Remarks;
                            appointment.ResponseSuccess = scheduleResponse.Result.ResponseSuccess;
                            appointment.ResponseMessage = scheduleResponse.Result.ResponseMessage;
                        }
                    }
                }

                apiResponse.Results = appointments;
                ApiResponseHelper.SetSuccessResponse(apiResponse, appointments, "Appointments retrieved successfully.");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "Error while retrieving appointments.");
            }

            return Ok(apiResponse);
        }


        [Authorize(Policy = PermissionConstants.AppointmentGetById)]
        [HttpGet("get-by-id")]
        public async Task<ActionResult<ApiResponse<AppointmentApiResponseDto>>> GetAppointmentById(int id)
        {
            var apiResponse = new ApiResponse<AppointmentApiResponseDto>();
            try
            {
                var appointment = await _appointmentService.GetById(id);
                if (appointment.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Appointment not found.");
                    return Ok(apiResponse);
                }

                var mappedAppointment = await _mapperService.MapSingle<Entities.EntityClass.Appointment, AppointmentApiResponseDto>(appointment.Result);
                apiResponse.Results = mappedAppointment;
                ApiResponseHelper.SetSuccessResponse(apiResponse, mappedAppointment, "Appointment retrieved successfully.");
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, "Error while retrieving appointment.");
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AppointmentCreate)]
        [HttpPost("create_appointment")]
        public async Task<ActionResult<ApiResponse<int>>> CreateAppointment(AppointmentInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _appointmentService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Appointment created successfully.");
                        return Ok(apiResponse);
                    }
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Invalid request data.");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Error while creating appointment.");
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AppointmentUpdate)]
        [HttpPut("update")]
        public async Task<ActionResult<ApiResponse<int>>> UpdateAppointment(AppointmentUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _appointmentService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Appointment updated successfully.");
                        return Ok(apiResponse);
                    }
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Invalid request data.");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, "Error while updating appointment.");
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.AppointmentDelete)]
        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAppointment(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _appointmentService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, true, "Appointment deleted successfully.");
                    return Ok(apiResponse);
                }
                ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, "Error while deleting appointment.");
            }
            return Ok(apiResponse);
        }


    }
}
