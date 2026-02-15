using AdviceTranslations.DatabaseModels;
using AdviceTranslations.Domain.Repositories.AdviceTranslations;
using AdviceTranslations.Utility;
using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace DoctorPrescription.Insfracture.RepositoriesImplement.AdviceTranslation
{
    public class AdviceTranslationsCommandRepository : IAdviceTranslationsCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public AdviceTranslationsCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new AdviceTranslationsDeleteModel
                {
                    TranslationId = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<AdviceTranslationsDeleteModel, AdviceTranslationsDeleteModel>(
                    "AdviceTranslations_DeleteById", 
                    deleteModel
                );




                ResponseHelper.SetSuccessResponse(response, true, AdviceTranslationsResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.AdviceTranslation entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new AdviceTranslationsInsertModel
                {
                    TranslationId = 0, // SP accepts but doesn't use (auto-generated)
                    AdviceId = entity.AdviceID,
                    LanguageId = entity.LanguageID,
                    TranslatedAdvice = entity.TranslatedAdvice,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<AdviceTranslationsInsertModel>(
                    "AdviceTranslations_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, 0, AdviceTranslationsResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, AdviceTranslationsResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.AdviceTranslation entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new AdviceTranslationsUpdateModel
                {
                    TranslationId = entity.TranslationID,
                    AdviceId = entity.AdviceID > 0 ? entity.AdviceID : null,
                    LanguageId = entity.LanguageID > 0 ? entity.LanguageID : null,
                    TranslatedAdvice = entity.TranslatedAdvice,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<AdviceTranslationsUpdateModel>(
                    "AdviceTranslations_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, 0, AdviceTranslationsResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, AdviceTranslationsResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
