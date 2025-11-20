using Appointment.Domain.Repositories.Appointment;
using Appointment.Dtos.RequestDto.AppointmentDto;
using Appointment.Utility;
using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using Pharmacies.Utility;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Appointment.Infrastructure.RepositoriesImplement
{
    public class AppointmentCommandRepository : IAppointmentCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public AppointmentCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Appointment, dynamic>(
                    "Appointment_DeleteById",
                    new { AppointmentId = id }
                );

                ResponseHelper.SetSuccessResponse(
                    response,
                    true,
                    AppointmentResponseMessage.common_delete_success_message,
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(
                    response,
                    false,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Appointment entity)
        {
            var response = new Response<int>();
            try
            {

                var appointmentDto = new AppointmentInsertRequestDto
                {
                    PatientName = entity.PatientName,
                    Gender = entity.Gender,
                    Age = entity.Age,
                    PhoneNumber = entity.PhoneNumber,
                    SessionId = entity.SessionId,
                    ScheduleId = entity.ScheduleId,
                    BloodGroup = entity.BloodGroup,
                    AppointmentDate = entity.AppointmentDate
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<AppointmentInsertRequestDto>(
                    "Appointment_Insert",
                    appointmentDto
                );

                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        result,
                        AppointmentResponseMessage.common_inserted_failed_message,
                        StatusResponseMessage.failed,
                        StatusCodes.Status400BadRequest
                    );
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(
                        response,
                        result,
                        AppointmentResponseMessage.common_insert_success_message,
                        StatusResponseMessage.success,
                        StatusCodes.Status201Created
                    );
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(
                    response,
                    0,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Appointment entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.Appointment>(
                    "Appointment_Update",
                    entity
                );

                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(
                        response,
                        result,
                        AppointmentResponseMessage.common_update_failed_message,
                        StatusResponseMessage.failed,
                        StatusCodes.Status400BadRequest
                    );
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(
                        response,
                        result,
                        AppointmentResponseMessage.common_update_success_message,
                        StatusResponseMessage.success,
                        StatusCodes.Status200OK
                    );
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(
                    response,
                    0,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
            }

            return response;
        }
    }
}
