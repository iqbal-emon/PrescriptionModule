using DataAccess.DatabaseAccessLayer;
using Diagononosis.Utility;
using Diagonosis.Domain.Repositories.Diagonosis;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Diagonosis.Insfracture.RepositoriesImplement.Diagnonosis
{

    public class DiagonosisCommandRepository : IDiagonosisCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DiagonosisCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a Prescription Diagnosis by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Diagonosis, dynamic>("Diagnosis_DeleteById", new
                {
                    DiagnosisID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, DiagononosisResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Insert a new Prescription Diagnosis
        public async Task<Response<int>> Insert(Entities.EntityClass.Diagonosis entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("Diagonosis_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DiagononosisResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DiagononosisResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing  Diagnosis
        public async Task<Response<int>> Update(Entities.EntityClass.Diagonosis entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("Diagonosis_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DiagononosisResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DiagononosisResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
