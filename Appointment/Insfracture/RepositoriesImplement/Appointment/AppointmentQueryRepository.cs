using Appointment.Domain.Repositories.Appointment;
using Appointment.Dtos.ResponseDto.AppointmentDto;
using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Pharmacies.Utility;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Appointment.Infrastructure.RepositoriesImplement
{
    public class AppointmentQueryRepository : IAppointmentQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public AppointmentQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<AppointmentApiResponseDto>>> GetAll()
        {
            var response = new Response<List<AppointmentApiResponseDto>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<AppointmentApiResponseDto, dynamic>(
                    "Appointment_GetAll",
                    new { }
                );

                response.Result = result.ToList();
                response.IsSuccess = true;

                ResponseHelper.SetSuccessResponse(
                    response,
                    result,
                    AppointmentResponseMessage.common_get_all_success,
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.Appointment>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Appointment>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Appointment, dynamic>(
                    "Appointment_GetById",
                    new { AppointmentId = id }
                );

                response.Result = result;
                response.IsSuccess = true;

                ResponseHelper.SetSuccessResponse(
                    response,
                    result,
                    AppointmentResponseMessage.common_get_by_id_success,
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
            }

            return response;
        }

        Task<Response<List<Entities.EntityClass.Appointment>>> IBaseCommonQueryMethodRepository<Entities.EntityClass.Appointment>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
