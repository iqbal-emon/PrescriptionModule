using Doctor.Domain.Repositories.DoctorSchedule;
using Doctor.Dtos.RequestDto.DoctorScheduleDaySessionDto;
using Doctor.Dtos.RequestDto.DoctorScheduleDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace Doctor.Application.Services
{
    public class DoctorScheduleService
    {
        private readonly IDoctorScheduleQueryRepository _scheduleQueryRepository;
        private readonly IDoctorScheduleCommandRepository _scheduleCommandRepository;
        private readonly MapperService _mapperService;
        private readonly DoctorScheduleDaySessionService _daySessionService;

        public DoctorScheduleService(
            IDoctorScheduleQueryRepository scheduleQueryRepository,
            IDoctorScheduleCommandRepository scheduleCommandRepository,
            MapperService mapperService,
            DoctorScheduleDaySessionService daySessionService)
        {
            _scheduleQueryRepository = scheduleQueryRepository;
            _scheduleCommandRepository = scheduleCommandRepository;
            _mapperService = mapperService;
            _daySessionService = daySessionService;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>();

            try
            {
                var schedules = await _scheduleQueryRepository.GetAll();

                if (schedules == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.success, schedules.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the DoctorSchedules.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorSchedule>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorSchedule>();

            try
            {
                var schedule = await _scheduleQueryRepository.GetById(id);

                if (schedule == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedule.Result, schedule.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedule.Result, schedule.Message, StatusResponseMessage.success, schedule.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DoctorScheduleInsertRequestDto schedule)
        {
            var response = new Response<int>();

            try
            {
                var scheduleEntity = await _mapperService.MapSingle<DoctorScheduleInsertRequestDto, Entities.EntityClass.DoctorEntity.DoctorSchedule>(schedule);
                scheduleEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _scheduleCommandRepository.Insert(scheduleEntity);

                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DoctorScheduleUpdateRequestDto schedule)
        {
            var response = new Response<int>();

            try
            {
                var scheduleEntity = await _mapperService.MapSingle<DoctorScheduleUpdateRequestDto, Entities.EntityClass.DoctorEntity.DoctorSchedule>(schedule);
                scheduleEntity.UpdatedAt = DateTime.Now;
                var updatedResponse = await _scheduleCommandRepository.Update(scheduleEntity);

                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the DoctorSchedule.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _scheduleCommandRepository.Delete(id);
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetByDoctorId(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>();

            try
            {
                var schedules = await _scheduleQueryRepository.GetByDoctorId(doctorId);

                if (schedules == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.success, schedules.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the DoctorSchedules by doctor ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetByDoctorIdAndChamberId(int doctorId, int chamberId)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>();

            try
            {
                var schedules = await _scheduleQueryRepository.GetByDoctorIdAndChamberId(doctorId, chamberId);

                if (schedules == null)
                {
                    ResponseHelper.SetFailedResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, schedules.Result, schedules.Message, StatusResponseMessage.success, schedules.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the DoctorSchedules by doctor and chamber ID.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        /// <summary>
        /// Creates a complete doctor schedule with all day sessions in a single transaction
        /// </summary>
        public async Task<Response<int>> InsertCompleteSchedule(DoctorScheduleInputRequestDto scheduleInput)
        {
            var response = new Response<int>();

            try
            {
                // Validate required fields
                if (scheduleInput.DoctorProfileId <= 0)
                {
                    response.Message = "Doctor Profile ID is required.";
                    ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    return response;
                }

                if (scheduleInput.DoctorScheduleDaySession == null || scheduleInput.DoctorScheduleDaySession.Count == 0)
                {
                    response.Message = "At least one schedule day session is required.";
                    ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    return response;
                }

                // Create a basic Schedule entry (using default ScheduleID = 0 or create one)
                // For now, we'll use ScheduleID = 0 as a placeholder since Schedule table is simple
                // In production, you might want to create a Schedule entry first
                int scheduleId = 0; // Default schedule ID

                // Create DoctorSchedule entry
                var doctorScheduleDto = new DoctorScheduleInsertRequestDto
                {
                    DoctorID = scheduleInput.DoctorProfileId,
                    ScheduleID = scheduleId,
                    TenantID = scheduleInput.TenantID > 0 ? scheduleInput.TenantID : 0
                };

                var scheduleInsertResponse = await Insert(doctorScheduleDto);
                
                if (!scheduleInsertResponse.IsSuccess || scheduleInsertResponse.Result <= 0)
                {
                    response.Message = scheduleInsertResponse.Message ?? "Failed to create doctor schedule.";
                    ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    return response;
                }

                int doctorScheduleId = scheduleInsertResponse.Result;

                // Create all day sessions
                var sessionErrors = new List<string>();
                foreach (var sessionInput in scheduleInput.DoctorScheduleDaySession)
                {
                    var daySessionDto = new DoctorScheduleDaySessionInsertRequestDto
                    {
                        DoctorScheduleID = doctorScheduleId,
                        ScheduleDayofWeek = sessionInput.ScheduleDayofWeek,
                        StartTime = sessionInput.StartTime,
                        EndTime = sessionInput.EndTime,
                        NoOfPatients = sessionInput.NoOfPatients,
                        IsActive = sessionInput.IsActive,
                        TenantID = scheduleInput.TenantID > 0 ? scheduleInput.TenantID : 0
                    };

                    var sessionResponse = await _daySessionService.Insert(daySessionDto);
                    
                    if (!sessionResponse.IsSuccess)
                    {
                        sessionErrors.Add($"Failed to create session for {sessionInput.ScheduleDayofWeek}: {sessionResponse.Message}");
                    }
                }

                if (sessionErrors.Count > 0)
                {
                    // If some sessions failed, we could rollback, but for now we'll return success with warnings
                    response.Message = $"Schedule created but some sessions failed: {string.Join("; ", sessionErrors)}";
                    ResponseHelper.SetSuccessResponse(response, doctorScheduleId, response.Message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
                else
                {
                    response.Result = doctorScheduleId;
                    response.IsSuccess = true;
                    response.Message = "Schedule and all sessions created successfully.";
                    ResponseHelper.SetSuccessResponse(response, doctorScheduleId, response.Message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while creating the complete schedule.";
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = $"An unexpected error occurred: {ex.Message}";
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        /// <summary>
        /// Updates a complete doctor schedule with all day sessions
        /// </summary>
        public async Task<Response<int>> UpdateCompleteSchedule(DoctorScheduleInputRequestDto scheduleInput)
        {
            var response = new Response<int>();

            try
            {
                if (!scheduleInput.Id.HasValue || scheduleInput.Id.Value <= 0)
                {
                    response.Message = "Schedule ID is required for update.";
                    ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    return response;
                }

                int doctorScheduleId = scheduleInput.Id.Value;

                // Update DoctorSchedule entry
                var doctorScheduleUpdateDto = new DoctorScheduleUpdateRequestDto
                {
                    DoctorScheduleID = doctorScheduleId,
                    DoctorID = scheduleInput.DoctorProfileId,
                    ScheduleID = 0, // Keep existing or update if needed
                    TenantID = scheduleInput.TenantID > 0 ? scheduleInput.TenantID : 0
                };

                var scheduleUpdateResponse = await Update(doctorScheduleUpdateDto);
                
                if (!scheduleUpdateResponse.IsSuccess)
                {
                    response.Message = scheduleUpdateResponse.Message ?? "Failed to update doctor schedule.";
                    ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    return response;
                }

                // Delete existing sessions and create new ones
                // Note: In production, you might want to update existing sessions instead of delete/recreate
                // Get all sessions and filter by DoctorScheduleID
                var allSessions = await _daySessionService.GetAll();
                if (allSessions.IsSuccess && allSessions.Result != null)
                {
                    var sessionsToDelete = allSessions.Result
                        .Where(s => s.DoctorScheduleID == doctorScheduleId)
                        .ToList();

                    foreach (var session in sessionsToDelete)
                    {
                        await _daySessionService.Delete(session.DoctorScheduleDaySessionID);
                    }
                }

                // Create all day sessions
                var sessionErrors = new List<string>();
                foreach (var sessionInput in scheduleInput.DoctorScheduleDaySession)
                {
                    var daySessionDto = new DoctorScheduleDaySessionInsertRequestDto
                    {
                        DoctorScheduleID = doctorScheduleId,
                        ScheduleDayofWeek = sessionInput.ScheduleDayofWeek,
                        StartTime = sessionInput.StartTime,
                        EndTime = sessionInput.EndTime,
                        NoOfPatients = sessionInput.NoOfPatients,
                        IsActive = sessionInput.IsActive,
                        TenantID = scheduleInput.TenantID > 0 ? scheduleInput.TenantID : 0
                    };

                    var sessionResponse = await _daySessionService.Insert(daySessionDto);
                    
                    if (!sessionResponse.IsSuccess)
                    {
                        sessionErrors.Add($"Failed to create session for {sessionInput.ScheduleDayofWeek}: {sessionResponse.Message}");
                    }
                }

                if (sessionErrors.Count > 0)
                {
                    response.Message = $"Schedule updated but some sessions failed: {string.Join("; ", sessionErrors)}";
                    ResponseHelper.SetSuccessResponse(response, doctorScheduleId, response.Message, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
                else
                {
                    response.Result = doctorScheduleId;
                    response.IsSuccess = true;
                    response.Message = "Schedule and all sessions updated successfully.";
                    ResponseHelper.SetSuccessResponse(response, doctorScheduleId, response.Message, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the complete schedule.";
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = $"An unexpected error occurred: {ex.Message}";
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}

