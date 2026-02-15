using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.DatabaseModels;
using Prescription.Domain.Repositories.Prescription;
using Prescription.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.Prescription
{
    public class PrescriptionCommandRepository : IPrescriptionCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PrescriptionCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new PrescriptionDeleteModel
                {
                    PrescriptionID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<PrescriptionDeleteModel, PrescriptionDeleteModel>(
                    "Prescription_DeledeById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, PrescriptionResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
   public async Task<Response<int>> Insert(Entities.EntityClass.PrescriptionEntity.Prescription entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new PrescriptionInsertModel
                {
                    PrescriptionId = 0, // SP accepts but doesn't use (auto-generated)
                    TenantId = entity.TenantId,
                    PatientId = entity.PatientId,
                    DoctorId = entity.DoctorId,
                    IsHeader = entity.IsHeader,
                    IsPreHand = entity.isPreHand,
                    PatientFollowUpId = entity.PatientFollowUpId,
                    PharmacyId = entity.PharmacyId,
                    IssueDate = entity.IssueDate ?? DateTime.UtcNow,
                    ExpiryDate = entity.ExpiryDate,
                    Language = entity.Language,
                    StatusId = entity.StatusId,
                    FollowUpDate = entity.FollowUpDate,
                    IsArchived = entity.IsArchived ?? false,
                    AppointmentRefId = entity.AppointmentRefId,
                    PrescriptionCode = entity.PrescriptionCode,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionInsertModel>(
                    "Prescription_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PrescriptionResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.PrescriptionEntity.Prescription entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new PrescriptionUpdateModel
                {
                    PrescriptionId = entity.PrescriptionId,
                    TenantId = entity.TenantId > 0 ? entity.TenantId : null,
                    PatientId = entity.PatientId > 0 ? entity.PatientId : null,
                    DoctorId = entity.DoctorId > 0 ? entity.DoctorId : null,
                    PatientFollowUpId = entity.PatientFollowUpId > 0 ? entity.PatientFollowUpId : null,
                    PharmacyId = entity.PharmacyId > 0 ? entity.PharmacyId : null,
                    IssueDate = entity.IssueDate,
                    ExpiryDate = entity.ExpiryDate,
                    Language = entity.Language,
                    StatusId = entity.StatusId > 0 ? entity.StatusId : null,
                    FollowUpDate = entity.FollowUpDate,
                    IsArchived = entity.IsArchived,
                    IsHeader = entity.IsHeader,
                    AppointmentRefId = entity.AppointmentRefId > 0 ? entity.AppointmentRefId : null,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<PrescriptionUpdateModel>(
                    "Prescription_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PrescriptionResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
