using ApiCallService.BaseApiCallService;
using Appointment.Domain.Repositories.Appointment;
using Appointment.Dtos.RequestDto;
using Appointment.Dtos.RequestDto.AppointmentDto;
using Appointment.Dtos.ResponseDto.AppointmentDto;
using Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto;
using Doctor.Dtos.ResponseDto.DoctorScheduleDto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using static System.Collections.Specialized.BitVector32;

namespace Appointment.Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentQueryRepository _appointmentQueryRepository;
        private readonly IAppointmentCommandRepository _appointmentCommandRepository;
        private readonly MapperService _mapperService;
        private readonly IConfiguration _configuration;
        private string _apiBaseURL;
        private readonly IBaseRestClientApiService _baseRestClientApiService;

        public AppointmentService(
            IAppointmentQueryRepository appointmentQueryRepository,
            IAppointmentCommandRepository appointmentCommandRepository,
            MapperService mapperService,
            IConfiguration configuration, IBaseRestClientApiService baseRestClientApiService)
        {
            _appointmentQueryRepository = appointmentQueryRepository;
            _appointmentCommandRepository = appointmentCommandRepository;
            _mapperService = mapperService;
            _configuration = configuration;
            _apiBaseURL = _configuration.GetSection("GeneralSettings:Soowgood").Value;
            _baseRestClientApiService = baseRestClientApiService;
        }

        public async Task<Response<PagedWithResponse<List<AppointmentApiResponseDto>>>> GetAll(
          int doctorId,
    int pageNumber = 1,
    int pageSize = 10,
    string search = null,
    int? sessionId = null,
    int? scheduleId = null)
        {
            // The response object that will be returned
            var response = new Response<PagedWithResponse<List<AppointmentApiResponseDto>>>();

            try
            {
                // Call repository to get paged appointments
                var repoResponse = await _appointmentQueryRepository.GetAll(doctorId,
            pageNumber,
            pageSize,
            search,
            sessionId,
            scheduleId);

                // Null-safe check
                if (repoResponse == null || repoResponse.Result == null || !repoResponse.Result.Result.Any())
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        null,
                        repoResponse?.Message ?? "No appointments found",
                        StatusResponseMessage.failed,
                        StatusCodes.Status400BadRequest
                    );
                    return response;
                }

                // Extract appointments list and total count from repository response
                var appointmentsList = repoResponse.Result.Result; // List<AppointmentApiResponseDto>
                var totalCount = repoResponse.Result.TotalCount;

                // Wrap the result in PagedWithResponse
                var pagedResult = new PagedWithResponse<List<AppointmentApiResponseDto>>
                {
                    Result = appointmentsList,
                    TotalCount = totalCount,
                    IsSuccess = true,
                    Message = "Appointments retrieved successfully."
                };

                // Fill main response
                response.Result = pagedResult;
                response.IsSuccess = true;
                response.Message = pagedResult.Message;
            }
            catch (Exception ex)
            {
                response.Message = $"An unexpected error occurred while retrieving appointments. {ex.Message}";
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }

            return response;
        }




        // Get appointment by ID
        public async Task<Response<AppointmentResponseDto>> GetById(int id)
        {
            var response = new Response<AppointmentResponseDto>();

            try
            {
                var appointment = await _appointmentQueryRepository.GetByAppointmentId(id);

                if (appointment == null)
                {
                    ResponseHelper.SetFailedResponse(response, appointment.Result, appointment.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, appointment.Result, appointment.Message, StatusResponseMessage.success, appointment.StatusCode);
                }
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while retrieving the appointment.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Insert a new appointment
        public async Task<Response<int>> Insert(AppointmentInsertRequestDto appointmentDto)
        {
            var response = new Response<int>();

            try
            {
                // Pass DTO directly to repository since stored procedure handles patient creation/update
                response = await _appointmentCommandRepository.Insert(appointmentDto);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the appointment.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Update an existing appointment
        public async Task<Response<int>> Update(AppointmentUpdateRequestDto appointmentDto)
        {
            var response = new Response<int>();

            try
            {
                var appointmentEntity = await _mapperService.MapSingle<AppointmentUpdateRequestDto, Entities.EntityClass.Appointment>(appointmentDto);
                appointmentEntity.UpdatedAt = DateTime.Now;

                var updateResponse = await _appointmentCommandRepository.Update(appointmentEntity);
                response = updateResponse;
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the appointment.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Delete an appointment
        public Task<Response<bool>> Delete(int id)
        {
            return _appointmentCommandRepository.Delete(id);
        }


        public async Task<Response<SessionResponseDto>> GetBySessionId(int? sessionId)
        {
            var response = new Response<SessionResponseDto>();
            
            // Validate sessionId
            if (!sessionId.HasValue || sessionId.Value <= 0)
            {
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    "Invalid session ID provided.",
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
                return response;
            }

            try
            {
                var baseUrl = _apiBaseURL;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        null,
                        "API base URL is not configured.",
                        StatusResponseMessage.failed,
                        StatusCodes.Status500InternalServerError
                    );
                    return response;
                }

                var endPoint = $"api/2025-02/doctor-schedule-day-session/{sessionId}/session";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, null, 3, 1000);
                
                if (responseJson == null || string.IsNullOrWhiteSpace(responseJson.Content))
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        null,
                        "Empty response received from session API.",
                        StatusResponseMessage.failed,
                        StatusCodes.Status500InternalServerError
                    );
                    return response;
                }

                // Deserialize the API response wrapper
                var apiResponseWrapper = JsonConvert.DeserializeObject<ApiResponse<DoctorScheduleDaySessionApiResponseDto>>(responseJson.Content);
                
                if (apiResponseWrapper == null || apiResponseWrapper.Results == null)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        null,
                        "Failed to deserialize session response or session not found.",
                        StatusResponseMessage.failed,
                        StatusCodes.Status500InternalServerError
                    );
                    return response;
                }

                // Map DoctorScheduleDaySessionApiResponseDto to SessionResponseDto
                var sessionData = apiResponseWrapper.Results;
                var userData = new SessionResponseDto
                {
                    DoctorScheduleId = sessionData.DoctorScheduleID,
                    DoctorScheduleName = null, // Not available in DoctorScheduleDaySessionApiResponseDto
                    ScheduleDayofWeek = sessionData.ScheduleDayofWeek,
                    StartTime = sessionData.StartTime,
                    EndTime = sessionData.EndTime,
                    NoOfPatients = sessionData.NoOfPatients,
                    IsActive = sessionData.IsActive
                };

                response.Result = userData;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(
                    response,
                    userData,
                    "Session retrieved successfully.",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );

                return response;
            }
            catch (Exception ex)
            {
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    $"Error retrieving session: {ex.Message}",
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
                return response;
            }
        }


        public async Task<Response<ScheduleResponseDto>> GetBySchedule(int? scheduleId)
        {
            var response = new Response<ScheduleResponseDto>();
            
            // Validate scheduleId
            if (!scheduleId.HasValue || scheduleId.Value <= 0)
            {
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    "Invalid schedule ID provided.",
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
                return response;
            }

            try
            {
                var baseUrl = _apiBaseURL;
                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        null,
                        "API base URL is not configured.",
                        StatusResponseMessage.failed,
                        StatusCodes.Status500InternalServerError
                    );
                    return response;
                }

                var endPoint = $"api/2025-02/doctor-schedule/{scheduleId}";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
                
                if (responseJson == null || string.IsNullOrWhiteSpace(responseJson.Content))
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        null,
                        "Empty response received from schedule API.",
                        StatusResponseMessage.failed,
                        StatusCodes.Status500InternalServerError
                    );
                    return response;
                }

                // Deserialize the API response wrapper
                var apiResponseWrapper = JsonConvert.DeserializeObject<ApiResponse<DoctorScheduleApiResponseDto>>(responseJson.Content);
                
                if (apiResponseWrapper == null || apiResponseWrapper.Results == null)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        null,
                        "Failed to deserialize schedule response or schedule not found.",
                        StatusResponseMessage.failed,
                        StatusCodes.Status500InternalServerError
                    );
                    return response;
                }

                // Map DoctorScheduleApiResponseDto to ScheduleResponseDto
                // Note: DoctorScheduleApiResponseDto has limited properties, so we map what's available
                var scheduleData = apiResponseWrapper.Results;
                var userData = new ScheduleResponseDto
                {
                    DoctorProfileId = scheduleData.DoctorID,
                    DoctorName = null, // Not available in DoctorScheduleApiResponseDto
                    ScheduleTypeName = null, // Not available in DoctorScheduleApiResponseDto
                    ConsultancyTypeName = null, // Not available in DoctorScheduleApiResponseDto
                    DoctorChamberId = null, // Not available in DoctorScheduleApiResponseDto
                    Chamber = null, // Not available in DoctorScheduleApiResponseDto
                    IsActive = !scheduleData.IsDeleted, // Use IsDeleted as inverse of IsActive
                    Status = null, // Not available in DoctorScheduleApiResponseDto
                    OffDayFrom = null, // Not available in DoctorScheduleApiResponseDto
                    DayTextFrom = null, // Not available in DoctorScheduleApiResponseDto
                    OffDayTo = null, // Not available in DoctorScheduleApiResponseDto
                    DayTextTo = null, // Not available in DoctorScheduleApiResponseDto
                    Remarks = null, // Not available in DoctorScheduleApiResponseDto
                    ScheduleName = null, // Not available in DoctorScheduleApiResponseDto
                    ResponseSuccess = apiResponseWrapper.IsSuccess,
                    ResponseMessage = apiResponseWrapper.Message
                };

                response.Result = userData;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(
                    response,
                    userData,
                    "Schedule retrieved successfully.",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );

                return response;
            }
            catch (Exception ex)
            {
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    $"Error retrieving schedule: {ex.Message}",
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
                return response;
            }
        }








    }
}
