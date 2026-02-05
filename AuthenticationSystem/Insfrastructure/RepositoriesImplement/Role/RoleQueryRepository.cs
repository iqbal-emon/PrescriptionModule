using AuthenticationSystem.Domain.Repositories.Role;
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

namespace AuthenticationSystem.Insfrastructure.RepositoriesImplement.Role
{
    public class RoleQueryRepository : IRoleQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public RoleQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.Role>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Role>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Role, dynamic>("Role_GetAll", new { });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AuthResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.Role>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Role>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Role, dynamic>("Role_GetById", new
                {
                    Id = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AuthResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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

