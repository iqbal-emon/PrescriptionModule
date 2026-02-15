using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Pharmacies.DatabaseModels;
using Pharmacies.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using System;
using System.Threading.Tasks;
using Pharmacies.Domain.Repositories.Pharmacies;

namespace Pharmacies.Insfracture.RepositoriesImplement.Pharmacy
{
    public class PharmaciesCommandRepository : IPharmaciesCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PharmaciesCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new PharmacyDeleteModel
                {
                    PharmacyID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<PharmacyDeleteModel, PharmacyDeleteModel>(
                    "Pharmacy_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, PharmaciesResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Pharmacy entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new PharmacyInsertModel
                {
                    PharmacyID = 0, // SP accepts but doesn't use (auto-generated)
                    TenantId = entity.TenantId,
                    PharmacyName = entity.PharmacyName,
                    Address = entity.Address,
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PharmacyInsertModel>(
                    "Pharmacy_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PharmaciesResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PharmaciesResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);

                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Pharmacy entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new PharmacyUpdateModel
                {
                    PharmacyID = entity.PharmacyId,
                    TenantId = entity.TenantId > 0 ? entity.TenantId : null,
                    PharmacyName = entity.PharmacyName,
                    Address = entity.Address,
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PharmacyUpdateModel>(
                    "Pharmacy_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PharmaciesResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PharmaciesResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
