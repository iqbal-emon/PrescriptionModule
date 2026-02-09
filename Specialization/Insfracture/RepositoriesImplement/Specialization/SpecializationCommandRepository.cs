using DataAccess.DatabaseAccessLayer;
using Specialization.Domain.Repositories.Specialization;
using Specialization.Utility;
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

namespace Specialization.Insfracture.RepositoriesImplement.Specialization
{
    public class SpecializationCommandRepository : ISpecializationCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public SpecializationCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a Specialization by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Specialization, dynamic>("Specialization_DeleteById", new
                {
                    SpecializationID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, SpecializationResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Insert a new Specialization
        public async Task<Response<int>> Insert(Entities.EntityClass.Specialization entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("Specialization_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, SpecializationResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, SpecializationResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing Specialization
        public async Task<Response<int>> Update(Entities.EntityClass.Specialization entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("Specialization_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, SpecializationResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, SpecializationResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

