using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.DoctorScheduleDaySession;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorScheduleDaySession
{
    public class DoctorScheduleDaySessionCommandRepository : IDoctorScheduleDaySessionCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorScheduleDaySessionCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorScheduleDaySessionDeleteModel
                {
                    DoctorScheduleDaySessionID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession, DoctorScheduleDaySessionDeleteModel>("DoctorScheduleDaySession_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorScheduleDaySessionResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorScheduleDaySessionInsertModel
                {
                    DoctorScheduleID = entity.DoctorScheduleID,
                    ScheduleDayofWeek = entity.ScheduleDayofWeek,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    NoOfPatients = entity.NoOfPatients,
                    IsActive = entity.IsActive,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorScheduleDaySession_Insert", insertModel, "@DoctorScheduleDaySessionID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorScheduleDaySessionResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorScheduleDaySessionResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorScheduleDaySessionUpdateModel
                {
                    DoctorScheduleDaySessionID = entity.DoctorScheduleDaySessionID,
                    DoctorScheduleID = entity.DoctorScheduleID,
                    ScheduleDayofWeek = entity.ScheduleDayofWeek,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    NoOfPatients = entity.NoOfPatients,
                    IsActive = entity.IsActive,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorScheduleDaySession_Update", updateModel, "@UpdatedId");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorScheduleDaySessionResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorScheduleDaySessionResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

