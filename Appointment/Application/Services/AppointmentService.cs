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

        // Get all appointments
        public async Task<Response<List<AppointmentApiResponseDto>>> GetAll()
        {
            var response = new Response<List<AppointmentApiResponseDto>>();

            try
            {
                var appointments = await _appointmentQueryRepository.GetAll();

                if (appointments == null)
                {
                    ResponseHelper.SetFailedResponse(response, appointments.Result, appointments.Message, StatusResponseMessage.success, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, appointments.Result, appointments.Message, StatusResponseMessage.success, appointments.StatusCode);
                }
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while retrieving appointments.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        // Get appointment by ID
        public async Task<Response<Entities.EntityClass.Appointment>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Appointment>();

            try
            {
                var appointment = await _appointmentQueryRepository.GetById(id);

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
