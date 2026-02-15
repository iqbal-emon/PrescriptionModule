using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.DatabaseModels;
using Prescription.Domain.Repositories.PrescriptionItem;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.PrescriptionItem
{
    public class PrescriptionItemCommandRepository : IPrescriptionItemCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PrescriptionItemCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new PrescriptionItemDeleteModel
                {
                    PrescriptionItemId = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<PrescriptionItemDeleteModel, PrescriptionItemDeleteModel>(
                    "PrescriptionItem_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, PrescriptionItemResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.PrescriptionEntity.PrescriptionItem entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new PrescriptionItemInsertModel
                {
                    PrescriptionItemId = 0, // SP accepts but doesn't use (auto-generated)
                    PrescriptionId = entity.PrescriptionId,
                    MedicationId = entity.MedicationId,
                    Dosage = entity.Dosage,
                    Quantity = entity.Quantity,
                    Duration = entity.Duration,
                    Timing = entity.Timing,
                    Instructions = entity.Instructions,
                    MealTime = entity.MealTime,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionItemInsertModel>(
                    "PrescriptionItem_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PrescriptionItemResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionItemResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.PrescriptionEntity.PrescriptionItem entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new PrescriptionItemUpdateModel
                {
                    PrescriptionItemId = entity.PrescriptionItemId,
                    PrescriptionId = entity.PrescriptionId > 0 ? entity.PrescriptionId : null,
                    MedicationId = entity.MedicationId > 0 ? entity.MedicationId : null,
                    Dosage = entity.Dosage,
                    Duration = entity.Duration,
                    Timing = entity.Timing,
                    MealTime = entity.MealTime,
                    Quantity = entity.Quantity,
                    Instructions = entity.Instructions,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionItemUpdateModel>(
                    "PrescriptionItem_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PrescriptionItemResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionItemResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
