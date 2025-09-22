using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using PrescriptionInvestigation.Domain.Repositories.PrescriptionInvestigation;
using PrescriptionInvestigation.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace PrescriptionInvestigation.Insfracture.RepositoriesImplement.PrescriptionInvestigation
{

    public class PrescriptionInvestigationQueryRepository : IPrescriptionInvestigationQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionInvestigationQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PatientEntity.PrescriptionInvestigation, dynamic>("PrescriptionInvestigation_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionInvestigationResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PatientEntity.PrescriptionInvestigation, dynamic>("PrescriptionInvestigation_GetById", new
                {
                    PrescriptionInvestigationId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionInvestigationResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
