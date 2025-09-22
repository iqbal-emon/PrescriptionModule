using CommonHistory.Domain.Repositories.CommonHistory;
using CommonHistory.Utility;
using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace CommonHistory.Insfracture.CommonHistory
{
    public class CommonHistoryQueryRepository : ICommonHistoryQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public CommonHistoryQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all common histories from the database
        public async Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.CommonHistory>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CommonHistory, dynamic>("CommonHistory_GetAll", new { });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, CommonHistoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }


        //Get Bookmarks

        public async Task<Response<List<Entities.EntityClass.CommonHistory>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.CommonHistory>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CommonHistory, dynamic>("GetBookMarksHistoryByDoctorId", new {
                    DoctorReferenceID=doctorId
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, CommonHistoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }
        


        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        // Get all common histories by name
        public async Task<Response<List<Entities.EntityClass.CommonHistory>>> GetAllCommonHistoryByName(string CommonHistoryName)
        {
            var response = new Response<List<Entities.EntityClass.CommonHistory>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CommonHistory, dynamic>("CommonHistory_GetByName", new
                {
                    CommonHistoryName = CommonHistoryName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, CommonHistoryResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Get a common history by ID
        public async Task<Response<Entities.EntityClass.CommonHistory>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.CommonHistory>();

            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.CommonHistory, dynamic>("CommonHistory_GetById", new
                {
                    CommonHistoryID = id
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, CommonHistoryResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
