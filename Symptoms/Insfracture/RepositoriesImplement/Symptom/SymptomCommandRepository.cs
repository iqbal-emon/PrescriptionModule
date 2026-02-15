using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Symptoms.DatabaseModels;
using Symptoms.Utility;
using symtoms.Domain.Repositories.Systom;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Symptoms.Infrastructure.RepositoriesImplement.Symptom
{
    public class SymptomCommandRepository : ISymptomsCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public SymptomCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a symptom by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();

            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new SymptomDeleteModel
                {
                    SymptomID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<SymptomDeleteModel, SymptomDeleteModel>(
                    "Symptom_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, SymptomResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Insert a new symptom
        public async Task<Response<int>> Insert(Entities.EntityClass.Symptom entity)
        {
            var response = new Response<int>();

            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new SymptomInsertModel
                {
                    TenantID = entity.TenantID,
                    SymptomName = entity.SymptomName,
                    Description = entity.Description
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<SymptomInsertModel>(
                    "Symptom_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, SymptomResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, SymptomResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing symptom
        public async Task<Response<int>> Update(Entities.EntityClass.Symptom entity)
        {
            var response = new Response<int>();

            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new SymptomUpdateModel
                {
                    SymptomID = entity.SymptomID,
                    TenantID = entity.TenantID,
                    SymptomName = entity.SymptomName,
                    Description = entity.Description
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<SymptomUpdateModel>(
                    "Symptom_Update", 
                    updateModel
                ); 
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, SymptomResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, SymptomResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
