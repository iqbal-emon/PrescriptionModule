using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
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
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.FollowUp, dynamic>("FollowUp_DeleteById", new
                {
                    FollowUpId = followUpId
                });

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
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.FollowUp>("FollowUp_Insert", entity);
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
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.FollowUp>("FollowUp_Update", entity);
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
