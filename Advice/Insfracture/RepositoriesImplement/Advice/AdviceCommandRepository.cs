using Advice.DatabaseModels;
using Advice.Domain.Repositories.Advice;
using Advice.Utility;
using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Advice.Insfracture.RepositoriesImplement.Advice
{
    public class AdviceCommandRepository : IAdviceCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public AdviceCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete an advice by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();

            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new CommonAdviceDeleteModel
                {
                    CommonAdviceID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<CommonAdviceDeleteModel, CommonAdviceDeleteModel>(
                    "CommonAdvices_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, AdviceResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Insert a new advice
        public async Task<Response<int>> Insert(Entities.EntityClass.CommonAdvice entity)
        {
            var response = new Response<int>();

            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new CommonAdviceInsertModel
                {
                    Advice = entity.Advice,
                    Type = entity.Type,
                    IsActive = entity.IsActive,
                    Description = entity.Description,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<CommonAdviceInsertModel>(
                    "CommonAdvices_Insert", 
                    insertModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, AdviceResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, AdviceResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing advice
        public async Task<Response<int>> Update(Entities.EntityClass.CommonAdvice entity)
        {
            var response = new Response<int>();

            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new CommonAdviceUpdateModel
                {
                    CommonAdviceID = entity.CommonAdviceID,
                    Advice = entity.Advice,
                    Type = entity.Type,
                    IsActive = entity.IsActive
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<CommonAdviceUpdateModel>(
                    "CommonAdvices_Update", 
                    updateModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, AdviceResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, AdviceResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
