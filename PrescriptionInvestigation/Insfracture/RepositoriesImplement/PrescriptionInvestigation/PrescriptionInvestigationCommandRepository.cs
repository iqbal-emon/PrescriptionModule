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
    public class PrescriptionInvestigationCommandRepository : IPrescriptionInvestigationCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PrescriptionInvestigationCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PatientEntity.PrescriptionInvestigation, dynamic>("PrescriptionInvestigation_DeleteById", new
                {
                    PrescriptionInvestigationID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, PrescriptionInvestigationResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.PatientEntity.PrescriptionInvestigation entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>("PrescriptionInvestigation_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionInvestigationResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionInvestigationResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.PatientEntity.PrescriptionInvestigation entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.PatientEntity.PrescriptionInvestigation>("PrescriptionInvestigation_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, PrescriptionInvestigationResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PrescriptionInvestigationResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
