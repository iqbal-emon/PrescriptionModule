using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using User.DatabaseModels;
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
                // Create database model matching stored procedure parameters
                var deleteModel = new UserDeleteModel
                {
                    UserID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<UserDeleteModel, UserDeleteModel>(
                    "User_DeleteById", 
                    deleteModel
                );

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
                // Map entity to database model matching stored procedure parameters
                var insertModel = new UserInsertModel
                {
                    TenantID = entity.TenantID,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    FullName = entity.FullName,
                    UserName = entity.UserName,
                    Email = entity.Email,
                    PasswordHash = entity.PasswordHash,
                    UserType = entity.UserType,
                    PhoneNumber = entity.PhoneNumber,
                    ContactNo = entity.ContactNo,
                    RoleId = entity.RoleId,
                    IsActive = entity.IsActive,
                    IsDeleted = entity.IsDeleted,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    ReferenceUserId = entity.ReferenceUserId
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<UserInsertModel>(
                    "User_Insert", 
                    insertModel
                );
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
                // Map entity to database model matching stored procedure parameters
                var updateModel = new UserUpdateModel
                {
                    UserID = entity.UserID,
                    TenantID = entity.TenantID,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    FullName = entity.FullName,
                    UserName = entity.UserName,
                    Email = entity.Email,
                    PasswordHash = entity.PasswordHash,
                    UserType = entity.UserType,
                    PhoneNumber = entity.PhoneNumber,
                    ContactNo = entity.ContactNo,
                    RoleId = entity.RoleId,
                    IsActive = entity.IsActive,
                    IsDeleted = entity.IsDeleted,
                    ReferenceUserId = entity.ReferenceUserId
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<UserUpdateModel>(
                    "User_Update", 
                    updateModel
                );
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
