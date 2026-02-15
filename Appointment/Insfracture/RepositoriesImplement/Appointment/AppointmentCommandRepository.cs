using Appointment.DatabaseModels;
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
                // Create database model matching stored procedure parameters
                var deleteModel = new AppointmentDeleteModel
                {
                    AppointmentId = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<AppointmentDeleteModel, AppointmentDeleteModel>(
                    "Appointment_DeleteById",
                    deleteModel
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
            // This method is kept for interface compatibility but should not be used
            // Use the Insert(AppointmentInsertRequestDto) method instead
            throw new NotImplementedException("Use Insert(AppointmentInsertRequestDto) method instead. The Appointment entity no longer contains patient information fields.");
        }

        public async Task<Response<int>> Insert(AppointmentInsertRequestDto appointmentDto)
        {
            var response = new Response<int>();
            try
            {
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("📋 AppointmentCommandRepository.Insert - Starting");
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine($"📦 Appointment DTO: PatientName={appointmentDto.PatientName}, PhoneNumber={appointmentDto.PhoneNumber}, SessionId={appointmentDto.SessionId}, ScheduleId={appointmentDto.ScheduleId}, DoctorProfileId={appointmentDto.DoctorProfileId}, Age={appointmentDto.Age}, Gender={appointmentDto.Gender}, BloodGroup={appointmentDto.BloodGroup}, AppointmentDate={appointmentDto.AppointmentDate}");
                
                // Create anonymous object with exact parameter names matching stored procedure
                // This ensures only the required parameters are sent with correct names
                var procedureParams = new
                {
                    PatientName = appointmentDto.PatientName ?? string.Empty,
                    PhoneNumber = appointmentDto.PhoneNumber ?? string.Empty,
                    Gender = appointmentDto.Gender ?? string.Empty,
                    BloodGroup = appointmentDto.BloodGroup ?? string.Empty,
                    Age = appointmentDto.Age,
                    SessionId = appointmentDto.SessionId,
                    ScheduleId = appointmentDto.ScheduleId,
                    DoctorProfileId = appointmentDto.DoctorProfileId,
                    AppointmentDate = appointmentDto.AppointmentDate
                };
                
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType(
                    "Appointment_Insert",
                    procedureParams
                );

                Console.WriteLine($"✅ Stored procedure returned result: {result}");
                Console.WriteLine("═══════════════════════════════════════════════════════════");

                if (result == 0)
                {
                    Console.WriteLine("⚠️ Result is 0 - treating as failure");
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
                    Console.WriteLine($"✅ Success - SerialNo: {result}");
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
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine("❌ AppointmentCommandRepository.Insert - Exception");
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                Console.WriteLine($"❌ Exception Type: {ex.GetType().Name}");
                Console.WriteLine($"❌ Exception Message: {ex.Message}");
                Console.WriteLine($"❌ Stack Trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"❌ Inner Exception: {ex.InnerException.Message}");
                }
                Console.WriteLine("═══════════════════════════════════════════════════════════");
                
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
                // Map entity to database model matching stored procedure parameters
                var updateModel = new AppointmentUpdateModel
                {
                    Id = entity.Id,
                    SessionId = entity.SessionId,
                    ScheduleId = entity.ScheduleId,
                    PatientId = entity.PatientId,
                    AppointmentDate = entity.AppointmentDate,
                    DoctorProfileId = entity.DoctorProfileId
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<AppointmentUpdateModel>(
                    "Appointment_Update",
                    updateModel
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
