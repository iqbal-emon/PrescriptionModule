using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionPatientHistory;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.PrescriptionPatientHistory
{
    public class PrescriptionPatientHistoryCommandRepository : IPrescriptionPatientHistoryCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionPatientHistoryCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a prescription patient history by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();

            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory, dynamic>("PrescriptionPatientHistory_DeleteById", new
                {
                    PrescriptionPatientHistoryID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, PrescriptionPatientHistoryResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Insert a new prescription patient history
        public async Task<Response<int>> Insert(Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory entity)
        {
            var response = new Response<int>();

            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>("PrescriptionPatientHistory_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing prescription patient history
        public async Task<Response<int>> Update(Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory entity)
        {
            var response = new Response<int>();

            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.PrescriptionPatientHistory>("PrescriptionPatientHistory_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionPatientHistoryResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
