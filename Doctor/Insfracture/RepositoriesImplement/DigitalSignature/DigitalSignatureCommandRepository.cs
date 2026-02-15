using DataAccess.DatabaseAccessLayer;
using Doctor.DatabaseModels;
using Doctor.Domain.Repositories.DigitalSignature;
using Doctor.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DigitalSignature
{
    public class DigitalSignatureCommandRepository : IDigitalSignatureCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DigitalSignatureCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new DigitalSignatureDeleteModel
                {
                    DigitalSignatureID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<DigitalSignatureDeleteModel, DigitalSignatureDeleteModel>(
                    "DigitalSignature_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, DigitalSignatureResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DigitalSignature entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new DigitalSignatureInsertModel
                {
                    DigitalSignatureID = 0, // SP accepts but doesn't use (auto-generated)
                    DoctorID = entity.DoctorID,
                    FileName = entity.FileName,
                    OriginalFileName = entity.OriginalFileName,
                    FilePath = entity.FilePath,
                    FileSize = entity.FileSize,
                    MimeType = entity.MimeType,
                    IsActive = entity.IsActive,
                    TenantID = entity.TenantID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DigitalSignatureInsertModel>(
                    "DigitalSignature_Insert", 
                    insertModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DigitalSignatureResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DigitalSignatureResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DigitalSignature entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new DigitalSignatureUpdateModel
                {
                    DigitalSignatureID = entity.DigitalSignatureID,
                    DoctorID = entity.DoctorID > 0 ? entity.DoctorID : null,
                    FileName = entity.FileName,
                    OriginalFileName = entity.OriginalFileName,
                    FilePath = entity.FilePath,
                    FileSize = entity.FileSize,
                    MimeType = entity.MimeType,
                    IsActive = entity.IsActive,
                    TenantID = entity.TenantID > 0 ? entity.TenantID : null,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    IsDeleted = entity.IsDeleted,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DigitalSignatureUpdateModel>(
                    "DigitalSignature_Update", 
                    updateModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DigitalSignatureResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DigitalSignatureResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

