using AuthenticationSystem.Domain.Repositories.Company;
using AuthenticationSystem.Utility;
using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass.CompanyEntity;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace AuthenticationSystem.Insfrastructure.RepositoriesImplement.Company
{
    public class CompanyCommandRepository : ICompanyCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public CompanyCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.CompanyEntity.Company, dynamic>("Company_DeleteById", new
                {
                    Id = id
                });
                ResponseHelper.SetSuccessResponse(response, true, AuthResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.CompanyEntity.Company entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.CompanyEntity.Company>("Company_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, AuthResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, AuthResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.CompanyEntity.Company entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.CompanyEntity.Company>("Company_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, AuthResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, AuthResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

