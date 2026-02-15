using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using FollowUp.DatabaseModels;
using FollowUp.Domain.Repositories.FollowUp;
using FollowUp.Utility;

namespace FollowUp.Insfracture.RepositoriesImplement.FollowUp
{
    public class FollowUpCommandRepository : IFollowUpCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public FollowUpCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

     

        public async Task<Response<bool>> Delete(int followUpId)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new FollowUpDeleteModel
                {
                    FollowUpId = followUpId
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<FollowUpDeleteModel, FollowUpDeleteModel>(
                    "FollowUp_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, FollowUpResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

     

        public async Task<Response<int>> Insert(Entities.EntityClass.FollowUp entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new FollowUpInsertModel
                {
                    TenantId = entity.TenantId,
                    Name = entity.Name,
                    Description = entity.Description
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<FollowUpInsertModel>(
                    "FollowUp_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, FollowUpResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, FollowUpResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

      

        public async Task<Response<int>> Update(Entities.EntityClass.FollowUp entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new FollowUpUpdateModel
                {
                    FollowUpId = entity.FollowUpId,
                    TenantId = entity.TenantId,
                    Name = entity.Name,
                    Description = entity.Description
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<FollowUpUpdateModel>(
                    "FollowUp_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, FollowUpResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, FollowUpResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
