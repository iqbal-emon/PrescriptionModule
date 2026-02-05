using AuthenticationSystem.Domain.Repositories.Company;
using AuthenticationSystem.Utility;
using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass.CompanyEntity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace AuthenticationSystem.Insfrastructure.RepositoriesImplement.Company
{
    public class CompanyQueryRepository : ICompanyQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public CompanyQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.CompanyEntity.Company>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.CompanyEntity.Company>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CompanyEntity.Company, dynamic>("Company_GetAll", new { });
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

        public async Task<Response<Entities.EntityClass.CompanyEntity.Company>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.CompanyEntity.Company>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.CompanyEntity.Company, dynamic>("Company_GetById", new
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

