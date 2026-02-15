using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.DatabaseModels;
using Prescription.Domain.Repositories.PrescriptionInvestigation;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.PrescriptionInvestigation
{
    public class PrescriptionInvestigationCommandRepository : IPrescriptionInvestigationCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionInvestigationCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new PrescriptionInvestigationDeleteModel
                {
                    PrescriptionInvestigationID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<PrescriptionInvestigationDeleteModel, PrescriptionInvestigationDeleteModel>(
                    "PrescriptionInvestigation_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, PrescriptionInvestigationResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.PatientEntity.PrescriptionInvestigation entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new PrescriptionInvestigationInsertModel
                {
                    PrescriptionInvestigationID = 0, // SP accepts but doesn't use (auto-generated)
                    InvestigationID = entity.InvestigationID,
                    PrescriptionID = entity.PrescriptionID,
                    Description = entity.Description,
                    IsActive = entity.IsActive,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionInvestigationInsertModel>(
                    "PrescriptionInvestigation_Insert", 
                    insertModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionInvestigationResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionInvestigationResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.PatientEntity.PrescriptionInvestigation entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new PrescriptionInvestigationUpdateModel
                {
                    PrescriptionInvestigationId = entity.PrescriptionInvestigationID,
                    InvestigationID = entity.InvestigationID,
                    PrescriptionID = entity.PrescriptionID > 0 ? entity.PrescriptionID : null,
                    Description = entity.Description,
                    IsActive = entity.IsActive,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionInvestigationUpdateModel>(
                    "PrescriptionInvestigation_Update", 
                    updateModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionInvestigationResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionInvestigationResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
