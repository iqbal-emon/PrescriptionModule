using Advice.Domain.Repositories.Advice;
using Advice.Utility;
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

namespace Advice.Insfracture.RepositoriesImplement.Advice
{
    public class AdviceQueryRepository:IAdviceQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public AdviceQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all Advice Records
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.CommonAdvice>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CommonAdvice, dynamic>("CommonAdvices_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AdviceResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
        //Get Advice BookMark
        public async Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.CommonAdvice>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CommonAdvice, dynamic>("GetBookMarksAdviceByDoctorId", new
                {
                    DoctorReferenceID = doctorId
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AdviceResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Advice by ID
        public async Task<Response<Entities.EntityClass.CommonAdvice>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.CommonAdvice>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.CommonAdvice, dynamic>("CommonAdvices_GetById", new
                {
                    CommonAdviceID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AdviceResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Advice by Name
        public async Task<Response<List<Entities.EntityClass.CommonAdvice>>> GetAllAdviceByName(string adviceName)
        {
            var response = new Response<List<Entities.EntityClass.CommonAdvice>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.CommonAdvice, dynamic>("CommonAdvices_GetByName", new
                {
                    AdviceName = adviceName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AdviceResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
