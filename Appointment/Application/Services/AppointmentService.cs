using Appointment.Domain.Repositories.Appointment;
using Appointment.Dtos.RequestDto;
using Appointment.Dtos.RequestDto.AppointmentDto;
using Appointment.Dtos.ResponseDto.AppointmentDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Appointment.Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentQueryRepository _appointmentQueryRepository;
        private readonly IAppointmentCommandRepository _appointmentCommandRepository;
        private readonly MapperService _mapperService;

        public AppointmentService(
            IAppointmentQueryRepository appointmentQueryRepository,
            IAppointmentCommandRepository appointmentCommandRepository,
            MapperService mapperService)
        {
            _appointmentQueryRepository = appointmentQueryRepository;
            _appointmentCommandRepository = appointmentCommandRepository;
            _mapperService = mapperService;
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


    }
}
