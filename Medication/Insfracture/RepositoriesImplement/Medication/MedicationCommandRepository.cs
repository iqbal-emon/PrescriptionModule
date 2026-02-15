using DataAccess.DatabaseAccessLayer;
using Medication.DatabaseModels;
using Medication.Domain.Repositories.Medication;
using Medication.Utility;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Medication.Insfracture.RepositoriesImplement.Medication
{
    public class MedicationCommandRepository : IMedicationCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public MedicationCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new MedicationDeleteModel
                {
                    MedicationId = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<MedicationDeleteModel, MedicationDeleteModel>(
                    "Medication_DeledeById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, MedicationResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.MedicineEntity.Medication entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new MedicationInsertModel
                {
                    TenantId = entity.TenantId,
                    MedicationBrandId = entity.MedicationBrandId,
                    GenericName = entity.GenericName,
                    DAR = entity.DAR,
                    MedicationName = entity.MedicationName,
                    Description = entity.Description,
                    Manufacturer = entity.Manufacturer,
                    DosageForm = entity.DosageForm,
                    Strength = entity.Strength,
                    Indication = entity.Indication,
                    IsActive = entity.IsActive,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<MedicationInsertModel>(
                    "Medication_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, MedicationResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, MedicationResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.MedicineEntity.Medication entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new MedicationUpdateModel
                {
                    MedicationId = entity.MedicationId,
                    TenantId = entity.TenantId,
                    MedicationBrandId = entity.MedicationBrandId,
                    GenericName = entity.GenericName,
                    DAR = entity.DAR,
                    MedicationName = entity.MedicationName,
                    Description = entity.Description,
                    Manufacturer = entity.Manufacturer,
                    DosageForm = entity.DosageForm,
                    Strength = entity.Strength,
                    Indication = entity.Indication,
                    IsActive = entity.IsActive
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<MedicationUpdateModel>(
                    "Medication_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, MedicationResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, MedicationResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
