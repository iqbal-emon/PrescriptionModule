using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionDiagonosis;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.PrescriptionDiagonosis
{
    public class PrescriptionDiagonsisCommandRepository : IPrescriptionDiagonsisCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PrescriptionDiagonsisCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis, dynamic>("PrescriptionDiagonosis_DeleteById", new
                {
                    DiagnosisId = id
                });

                ResponseHelper.SetSuccessResponse(response, true, PrescriptionExaminationResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>("PrescriptionDiagnoses_Insert", entity);
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, 0, PrescriptionExaminationResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionExaminationResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PrescriptionEntity.PrescriptionDiagnosis>("PrescriptionDiagnoses_Update", entity);
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PrescriptionExaminationResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionExaminationResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
