using Appointment.Domain.Repositories.Appointment;
using Appointment.Dtos.ResponseDto.AppointmentDto;
using Dapper;
using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Pharmacies.Utility;
using System.Data;
using System.Data.SqlClient;
using Utility;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Appointment.Infrastructure.RepositoriesImplement
{
    public class AppointmentQueryRepository : IAppointmentQueryRepository
    {
        private readonly string _connectionString = AppSettings.ConnectionStringForDapper;
        private readonly ISqlDataAccessLayer _dataAccess;

        public AppointmentQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<AppointmentApiResponseDto>>> GetAll(int doctorId)
        {
            var response = new Response<List<AppointmentApiResponseDto>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<AppointmentApiResponseDto, dynamic>(
                    "Appointment_GetAll",
                    new { doctorId=doctorId }
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
        public async Task<Response<PagedWithResponse<List<AppointmentApiResponseDto>>>> GetAll(
                    int doctorId,
    int pageNumber = 1,
    int pageSize = 10,
    string search = null,
    int? sessionId = null,
    int? scheduleId = null)
        {
            var response = new Response<PagedWithResponse<List<AppointmentApiResponseDto>>>();

            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();
                parameters.Add("@DoctorId", doctorId);
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@SearchText", search);
                parameters.Add("@sessionId ", sessionId);
                parameters.Add("@scheduleId ", scheduleId);
                parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute stored procedure and map data
                var appointments = (await connection.QueryAsync<AppointmentApiResponseDto>(
                    "Appointment_GetAll",
                    parameters,
                    commandType: CommandType.StoredProcedure
                )).ToList();

                // Read the TotalCount from the output parameter
                int totalCount = parameters.Get<int>("@TotalCount");

                // Wrap results in PagedWithResponse
                var pagedResult = new PagedWithResponse<List<AppointmentApiResponseDto>>
                {
                    Result = appointments,
                    TotalCount = totalCount,
                    IsSuccess = true,
                    Message = "Appointments retrieved successfully.",
                };

                // Fill main response
                response.Result = pagedResult;
                response.IsSuccess = true;
                response.Message = "Appointments retrieved successfully.";

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




        public async Task<Response<AppointmentResponseDto>> GetByAppointmentId(int id)
        {
            var response = new Response<AppointmentResponseDto>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<AppointmentResponseDto, dynamic>(
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

        public Task<Response<Entities.EntityClass.Appointment>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<Response<List<Entities.EntityClass.Appointment>>> IBaseCommonQueryMethodRepository<Entities.EntityClass.Appointment>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
