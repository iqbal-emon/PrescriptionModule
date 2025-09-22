using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.ScannedPrescription;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.ScannedPrescription
{
    public class ScannedPrescriptionCommandRepository : IScannedPrescrptionCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public ScannedPrescriptionCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.ScannedPrescription, dynamic>("ScannedPrescription_DeleteById", new
                {
                    ScannedPrescriptionId = id
                });

                ResponseHelper.SetSuccessResponse(response, true, ScannedPrescriptionResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.PrescriptionEntity.ScannedPrescription entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>("ScannedPrescription_Insert", entity);
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, ScannedPrescriptionResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, ScannedPrescriptionResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.PrescriptionEntity.ScannedPrescription entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>("ScannedPrescription_Update", entity);
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, ScannedPrescriptionResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, ScannedPrescriptionResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
