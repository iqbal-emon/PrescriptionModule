using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.DoctorScheduledDayOff;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorScheduledDayOff
{
    public class DoctorScheduledDayOffCommandRepository : IDoctorScheduledDayOffCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorScheduledDayOffCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorScheduledDayOffDeleteModel
                {
                    DoctorScheduledDayOffID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff, DoctorScheduledDayOffDeleteModel>("DoctorScheduledDayOff_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorScheduledDayOffResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorScheduledDayOffInsertModel
                {
                    DoctorScheduleID = entity.DoctorScheduleID,
                    OffDay = entity.OffDay,
                    IsActive = entity.IsActive
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorScheduledDayOff_Insert", insertModel, "@DoctorScheduledDayOffID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorScheduledDayOffResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorScheduledDayOffResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorScheduledDayOffUpdateModel
                {
                    DoctorScheduledDayOffID = entity.DoctorScheduledDayOffID,
                    DoctorScheduleID = entity.DoctorScheduleID,
                    OffDay = entity.OffDay,
                    IsActive = entity.IsActive
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorScheduledDayOff_Update", updateModel, "@UpdatedId");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorScheduledDayOffResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorScheduledDayOffResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

