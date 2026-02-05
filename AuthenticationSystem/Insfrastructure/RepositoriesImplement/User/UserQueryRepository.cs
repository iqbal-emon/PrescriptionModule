using AuthenticationSystem.Domain.Repositories.User;
using AuthenticationSystem.Utility;
using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace AuthenticationSystem.Insfrastructure.RepositoriesImplement.User
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public UserQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_GetAll", new { });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, result, AuthResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.User>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.User>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_GetById", new
                {
                    UserID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse<Entities.EntityClass.User>(response, result, AuthResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.User>> GetByEmail(string email)
        {
            var response = new Response<Entities.EntityClass.User>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_GetByEmail", new
                {
                    Email = email
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse<Entities.EntityClass.User>(response, result, AuthResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetByTenantId(int tenantId)
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_GetByTenantId", new
                {
                    TenantID = tenantId
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, result, AuthResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetByUserType(string userType)
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_GetByUserType", new
                {
                    UserType = userType
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, result, AuthResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.User>> GetByUserName(string userName)
        {
            var response = new Response<Entities.EntityClass.User>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_GetByUserName", new
                {
                    UserName = userName
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse<Entities.EntityClass.User>(response, result, AuthResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse<Entities.EntityClass.User>(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.User>>> GetByRoleId(int roleId)
        {
            var response = new Response<List<Entities.EntityClass.User>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.User, dynamic>("User_GetByRoleId", new
                {
                    RoleId = roleId
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse<List<Entities.EntityClass.User>>(response, result, AuthResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse<List<Entities.EntityClass.User>>(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
    }
}

