using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using Doctor.Domain.Repositories.DoctorSchedule;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorSchedule
{
    public class DoctorScheduleCommandRepository : IDoctorScheduleCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorScheduleCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorScheduleDeleteModel
                {
                    DoctorScheduleID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorSchedule, DoctorScheduleDeleteModel>("DoctorSchedule_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorScheduleResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorSchedule entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorScheduleInsertModel
                {
                    DoctorScheduleID = entity.DoctorScheduleID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    ScheduleID = entity.ScheduleID,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorScheduleInsertModel>("DoctorSchedule_Insert", insertModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorScheduleResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorScheduleResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorSchedule entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorScheduleUpdateModel
                {
                    DoctorScheduleID = entity.DoctorScheduleID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    ScheduleID = entity.ScheduleID,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted,
                    CreatedAt = entity.CreatedAt
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorScheduleUpdateModel>("DoctorSchedule_Update", updateModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorScheduleResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorScheduleResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

