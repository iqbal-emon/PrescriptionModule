using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using User.Domain.Repositories.User;
using User.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace User.Infrastructure.RepositoriesImplement.User
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public UserCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a user by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();

            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_DeleteById", new
                {
                    UserId = id
                });

                ResponseHelper.SetSuccessResponse(response, true, UserResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Insert a new user
        public async Task<Response<int>> Insert(Entities.EntityClass.User entity)
        {
            var response = new Response<int>();

            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.User>("User_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, UserResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, UserResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing user
        public async Task<Response<int>> Update(Entities.EntityClass.User entity)
        {
            var response = new Response<int>();

            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.User>("User_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, UserResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, UserResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }
    }
}
