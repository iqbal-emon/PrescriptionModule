using DataAccess.DatabaseAccessLayer;
using Investigation.DatabaseModels;
using Investigation.Domain.Repositories.Investigation;
using Investigation.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Investigation.Insfracture.RepositoriesImplement.Investigation
{
    public class InvestigationCommandRepository : IInvestigationCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public InvestigationCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new InvestigationDeleteModel
                {
                    InvestigationID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<InvestigationDeleteModel, InvestigationDeleteModel>(
                    "Investigation_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, InvestigationResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Investigation entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new InvestigationInsertModel
                {
                    InvestigationID = 0, // SP accepts but doesn't use (auto-generated)
                    Name = entity.Name,
                    Description = entity.Description,
                    Code = entity.Code,
                    // CreatedAt, UpdatedAt, IsDeleted, IsActive are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<InvestigationInsertModel>(
                    "Investigations_Insert", 
                    insertModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, InvestigationResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, InvestigationResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Investigation entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new InvestigationUpdateModel
                {
                    InvestigationID = entity.InvestigationID,
                    Name = entity.Name,
                    Description = entity.Description,
                    Code = entity.Code,
                    IsActive = entity.IsActive,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<InvestigationUpdateModel>(
                    "Investigations_Update", 
                    updateModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, InvestigationResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, InvestigationResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
