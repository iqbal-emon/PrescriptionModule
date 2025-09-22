using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using DoctorSchedule.Domain.Repositories.DoctorSchedule;

namespace DoctorSchedule.Insfracture.RepositoriesImplement.DoctorSchedule
{
    public class DoctorScheduleCommandRepository : IDoctorScheduleCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorScheduleCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a Doctor Schedule by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorSchedule, dynamic>("DoctorSchedule_DeleteById", new
                {
                    DoctorScheduleID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, DoctorScheduleResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Insert a new Doctor Schedule
        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorSchedule entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("DoctorSchedule_Insert", entity);
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

        // Update an existing Doctor Schedule
        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorSchedule entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("DoctorSchedule_Update", entity);
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