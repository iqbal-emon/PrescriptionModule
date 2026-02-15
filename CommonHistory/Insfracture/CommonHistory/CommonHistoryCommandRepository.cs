using CommonHistory.DatabaseModels;
using CommonHistory.Domain.Repositories.CommonHistory;
using CommonHistory.Utility;
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

namespace CommonHistory.Insfracture.CommonHistory
{
    public class CommonHistoryCommandRepository : ICommonHistoryCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public CommonHistoryCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a common history by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();

            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new CommonHistoryDeleteModel
                {
                    CommonHistoryId = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<CommonHistoryDeleteModel, CommonHistoryDeleteModel>(
                    "CommonHistory_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, CommonHistoryResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Insert a new common history
        public async Task<Response<int>> Insert(Entities.EntityClass.CommonHistory entity)
        {
            var response = new Response<int>();

            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new CommonHistoryInsertModel
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    IsActive = entity.IsActive
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<CommonHistoryInsertModel>(
                    "CommonHistory_Insert", 
                    insertModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, CommonHistoryResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, CommonHistoryResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing common history
        public async Task<Response<int>> Update(Entities.EntityClass.CommonHistory entity)
        {
            var response = new Response<int>();

            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new CommonHistoryUpdateModel
                {
                    CommonHistoryId = entity.CommonHistoryId,
                    Name = entity.Name,
                    Description = entity.Description,
                    IsActive = entity.IsActive
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<CommonHistoryUpdateModel>(
                    "CommonHistory_Update", 
                    updateModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, CommonHistoryResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, CommonHistoryResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
