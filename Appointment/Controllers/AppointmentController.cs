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
        [HttpGet("get-all")]
        public async Task<ActionResult<ApiResponse<List<AppointmentApiResponseDto>>>> GetAllAppointments()
        {
            var apiResponse = new ApiResponse<List<AppointmentApiResponseDto>>();
            try
            {
                var appointments = await _appointmentService.GetAll();
                var mappedAppointments = appointments.Result;

                if (appointments.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "No appointments found.");
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedAppointments;
                ApiResponseHelper.SetSuccessResponse(apiResponse, mappedAppointments, "Appointments retrieved successfully.");
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
