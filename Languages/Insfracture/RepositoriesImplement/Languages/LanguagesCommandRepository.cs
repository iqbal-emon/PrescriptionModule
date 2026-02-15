using DataAccess.DatabaseAccessLayer;
using Languages.DatabaseModels;
using Languages.Domain.Repositories.Languages;
using Languages.Utility;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Languages.Insfracture.RepositoriesImplement.Languages
{
    public class LanguagesCommandRepository : ILanguagesCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public LanguagesCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new LanguagesDeleteModel
                {
                    LanguageID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<LanguagesDeleteModel, LanguagesDeleteModel>(
                    "Languages_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, LanguagesResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Language entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new LanguagesInsertModel
                {
                    LanguageID = 0, // SP accepts but doesn't use (auto-generated)
                    LanguageCode = entity.LanguageCode,
                    LanguageName = entity.LanguageName,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<LanguagesInsertModel>(
                    "Languages_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, LanguagesResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, LanguagesResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Language entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new LanguagesUpdateModel
                {
                    LanguageID = entity.LanguageID,
                    LanguageCode = entity.LanguageCode,
                    LanguageName = entity.LanguageName,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<LanguagesUpdateModel>(
                    "Languages_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, LanguagesResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, LanguagesResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
