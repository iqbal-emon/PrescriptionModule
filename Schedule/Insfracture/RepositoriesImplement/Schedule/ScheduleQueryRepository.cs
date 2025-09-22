using DataAccess.DatabaseAccessLayer;
using Schedule.Domain.Repositories.Schedule;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using Schedule.Utility;
using Entities.EntityClass;

namespace Schedule.Insfracture.RepositoriesImplement.Schedule
{
    public class ScheduleQueryRepository : IScheduleQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public ScheduleQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.Schedule>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Schedule>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Schedule, dynamic>("Schedule_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ScheduleResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.Schedule>> GetByRoleAndReferenceId(int userId)
        {
            var response = new Response<Entities.EntityClass.Schedule>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Schedule, dynamic>("Schedule_GetByUserId", new
                {
                    UserId = userId,
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ScheduleResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.Schedule>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Schedule>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Schedule, dynamic>("Schedule_GetById", new
                {
                    ScheduleID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ScheduleResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
    }
}
