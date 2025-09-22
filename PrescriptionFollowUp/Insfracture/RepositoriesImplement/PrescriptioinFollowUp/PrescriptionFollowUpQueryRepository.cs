using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using PrescriptionFollowUp.Domain.Repositories.PrescriptionFollowUp;
using PrescriptionFollowUp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace PrescriptionFollowUp.Insfracture.RepositoriesImplement.PrescriptioinFollowUp
{
    public class PrescriptionFollowUpQueryRepository : IPrescriptionFollowUpQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PrescriptionFollowUpQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp, dynamic>("PrescriptionFollowUp_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionFollowUpResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp, dynamic>("PrescriptionFollowUp_GetById", new
                {
                    PrescriptionFollowUpId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionFollowUpResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>> GetPrescriptionFollowUpByName(string name, string uniCode)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionFollowUp, dynamic>("PrescriptionFollowUp_GetByName", new
                {
                    name = name,
                    uniCode = uniCode
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionFollowUpResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
