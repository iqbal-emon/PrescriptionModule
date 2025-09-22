using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;
using DoctorSchedule.Domain.Repositories.DoctorSchedule;

namespace DoctorSchedule.Insfracture.RepositoriesImplement.DoctorSchedule
{
    public class DoctorScheduleQueryRepository : IDoctorScheduleQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DoctorScheduleQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all Doctor Schedules
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorSchedule, dynamic>("DoctorSchedule_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorScheduleResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Doctor Schedule by ID
        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorSchedule>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorSchedule>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorSchedule, dynamic>("DoctorSchedule_GetById", new
                {
                    ScheduleID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorScheduleResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Doctor Schedules by Name
        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetAllScheduleName(string scheduleName)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorSchedule, dynamic>("DoctorSchedule_GetSchedulesByName", new
                {
                    ScheduleName = scheduleName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorScheduleResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }
    }
}
