using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
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
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Symptom, dynamic>("Symptom_DeleteById", new
                {
                    SymptomId = id
                });

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
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.Symptom>("Symptom_Insert", entity);
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
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.Symptom>("Symptom_Update", entity); 
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
