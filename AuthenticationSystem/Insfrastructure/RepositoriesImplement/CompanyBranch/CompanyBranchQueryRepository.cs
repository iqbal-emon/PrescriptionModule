using AuthenticationSystem.Domain.Repositories.CompanyBranch;
using AuthenticationSystem.Utility;
using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass.CompanyEntity;
using CompanyBranchEntity = Entities.EntityClass.CompanyEntity.CompanyBranch;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace AuthenticationSystem.Insfrastructure.RepositoriesImplement.CompanyBranch
{
    public class CompanyBranchQueryRepository : ICompanyBranchQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public CompanyBranchQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<CompanyBranchEntity>>> GetAll()
        {
            var response = new Response<List<CompanyBranchEntity>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<CompanyBranchEntity, dynamic>("CompanyBranch_GetAll", new { });
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

        public async Task<Response<CompanyBranchEntity>> GetById(int id)
        {
            var response = new Response<CompanyBranchEntity>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<CompanyBranchEntity, dynamic>("CompanyBranch_GetById", new
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

        public async Task<Response<List<CompanyBranchEntity>>> GetByCompanyId(int companyId)
        {
            var response = new Response<List<CompanyBranchEntity>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<CompanyBranchEntity, dynamic>("CompanyBranch_GetByCompanyId", new
                {
                    CompanyId = companyId
                });
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
    }
}

