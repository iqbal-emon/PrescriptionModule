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
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.CommonHistory, dynamic>("CommonHistory_DeleteById", new
                {
                    CommonHistoryId = id
                });

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
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.CommonHistory>("CommonHistory_Insert", entity);
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
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.CommonHistory>("CommonHistory_Update", entity);
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
