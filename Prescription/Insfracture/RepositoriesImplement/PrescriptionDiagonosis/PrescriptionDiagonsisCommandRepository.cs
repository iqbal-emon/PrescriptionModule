using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.DatabaseModels;
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
                // Create database model matching stored procedure parameters
                var deleteModel = new PrescriptionDiagonsisDeleteModel
                {
                    DiagnosisId = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<PrescriptionDiagonsisDeleteModel, PrescriptionDiagonsisDeleteModel>(
                    "PrescriptionDiagonosis_DeleteById", 
                    deleteModel
                );

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
                // Map entity to database model matching stored procedure parameters
                var insertModel = new PrescriptionDiagonsisInsertModel
                {
                    PrescriptionDiagnosisId = 0, // SP accepts but doesn't use (auto-generated)
                    PrescriptionId = entity.PrescriptionId,
                    DiagnosisID = entity.DiagnosisId,
                    Notes = entity.Notes,
                    PastDiagnosis = entity.PastDiagnosis,
                    PresentDiagnosis = entity.PresentDiagnosis,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionDiagonsisInsertModel>(
                    "PrescriptionDiagnoses_Insert", 
                    insertModel
                );
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
                // Map entity to database model matching stored procedure parameters
                var updateModel = new PrescriptionDiagonsisUpdateModel
                {
                    PrescriptionDiagnosisId = entity.PrescriptionDiagnosisId,
                    PrescriptionId = entity.PrescriptionId > 0 ? entity.PrescriptionId : null,
                    DiagnosisId = entity.DiagnosisId > 0 ? entity.DiagnosisId : null,
                    Notes = entity.Notes,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionDiagonsisUpdateModel>(
                    "PrescriptionDiagnoses_Update", 
                    updateModel
                );
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
