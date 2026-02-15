using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.DoctorFeesSetup;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorFeesSetup
{
    public class DoctorFeesSetupCommandRepository : IDoctorFeesSetupCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorFeesSetupCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorFeesSetupDeleteModel
                {
                    DoctorFeesSetupID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorFeesSetup, DoctorFeesSetupDeleteModel>("DoctorFeesSetup_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorFeesSetupResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorFeesSetup entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorFeesSetupInsertModel
                {
                    DoctorScheduleID = entity.DoctorScheduleID,
                    AppointmentType = entity.AppointmentType,
                    CurrentFee = entity.CurrentFee,
                    PreviousFee = entity.PreviousFee,
                    FeeAppliedFrom = entity.FeeAppliedFrom,
                    FollowUpPeriod = entity.FollowUpPeriod,
                    ReportShowPeriod = entity.ReportShowPeriod,
                    Discount = entity.Discount,
                    DiscountAppliedFrom = entity.DiscountAppliedFrom,
                    DiscountPeriod = entity.DiscountPeriod,
                    TotalFee = entity.TotalFee,
                    IsActive = entity.IsActive
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorFeesSetup_Insert", insertModel, "@DoctorFeesSetupID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorFeesSetupResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorFeesSetupResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorFeesSetup entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorFeesSetupUpdateModel
                {
                    DoctorFeesSetupID = entity.DoctorFeesSetupID,
                    DoctorScheduleID = entity.DoctorScheduleID,
                    AppointmentType = entity.AppointmentType,
                    CurrentFee = entity.CurrentFee,
                    PreviousFee = entity.PreviousFee,
                    FeeAppliedFrom = entity.FeeAppliedFrom,
                    FollowUpPeriod = entity.FollowUpPeriod,
                    ReportShowPeriod = entity.ReportShowPeriod,
                    Discount = entity.Discount,
                    DiscountAppliedFrom = entity.DiscountAppliedFrom,
                    DiscountPeriod = entity.DiscountPeriod,
                    TotalFee = entity.TotalFee,
                    IsActive = entity.IsActive
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorFeesSetup_Update", updateModel, "@UpdatedId");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorFeesSetupResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorFeesSetupResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

