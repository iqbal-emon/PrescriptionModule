using ApiCallService.BaseApiCallService;
using Appointment.Domain.Repositories.Appointment;
using Appointment.Dtos.RequestDto;
using Appointment.Dtos.RequestDto.AppointmentDto;
using Appointment.Dtos.ResponseDto.AppointmentDto;
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
                var appointmentEntity = await _mapperService.MapSingle<AppointmentInsertRequestDto, Entities.EntityClass.Appointment>(appointmentDto);
                appointmentEntity.CreatedAt = DateTime.Now;
                appointmentEntity.UpdatedAt = DateTime.Now;

                var insertResponse = await _appointmentCommandRepository.Insert(appointmentEntity);
                response = insertResponse;
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
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = $"api/app/doctor-schedule-day-session/{sessionId}/session";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                var userData = deSerializedJsonResult.ToObject<SessionResponseDto>();

                if (userData != null)
                {
                    response.Result = userData;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Response<ScheduleResponseDto>> GetBySchedule(int? scheduleId)
        {
            var response = new Response<ScheduleResponseDto>();
            try
            {
                var baseUrl = _apiBaseURL;
                var endPoint = $"api/app/doctor-schedule/{scheduleId}";

                // Add Authorization header
                string token = _configuration.GetSection("GeneralSettings:ApiAuthorizationToken").Value;
                var responseJson = await _baseRestClientApiService.MakeApiCall<JObject>(baseUrl, endPoint, Method.Get, null, token, 3, 1000);
                var deSerializedJsonResult = JsonConvert.DeserializeObject<JObject>(responseJson.Content);
                var userData = deSerializedJsonResult.ToObject<ScheduleResponseDto>();

                if (userData != null)
                {
                    response.Result = userData;
                    response.IsSuccess = true;
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }








    }
}
