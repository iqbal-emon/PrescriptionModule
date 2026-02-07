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

        // NOTE: This method is deprecated. Use the paginated GetAll method instead.
        // The stored procedure Appointment_GetAll requires pagination parameters.
        [Obsolete("Use GetAll(int doctorId, int pageNumber, int pageSize, string search, int? sessionId, int? scheduleId) instead")]
        public async Task<Response<List<AppointmentApiResponseDto>>> GetAll(int doctorId)
        {
            // Redirect to paginated version with default values
            var paginatedResponse = await GetAll(doctorId, 1, 1000, null, null, null);
            
            var response = new Response<List<AppointmentApiResponseDto>>();
            if (paginatedResponse?.Result?.Result != null)
            {
                response.Result = paginatedResponse.Result.Result;
                response.IsSuccess = paginatedResponse.IsSuccess;
                response.Message = paginatedResponse.Message;
                response.StatusCode = paginatedResponse.StatusCode;
            }
            else
            {
                response.Result = new List<AppointmentApiResponseDto>();
                response.IsSuccess = false;
                response.Message = "Failed to retrieve appointments";
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
                parameters.Add("@SessionId", sessionId);
                parameters.Add("@ScheduleId", scheduleId);
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
