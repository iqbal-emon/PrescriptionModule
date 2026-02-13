using DataAccess.DatabaseAccessLayer;
using Speciality.Domain.Repositories.Speciality;
using Speciality.Utility;
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

namespace Speciality.Insfracture.RepositoriesImplement.Speciality
{
    public class SpecialityCommandRepository : ISpecialityCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public SpecialityCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a Speciality by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Speciality, dynamic>("Speciality_DeleteById", new
                {
                    SpecialityID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, SpecialityResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Insert a new Speciality
        public async Task<Response<int>> Insert(Entities.EntityClass.Speciality entity)
        {
            var response = new Response<int>();
            try
            {
                // Create parameter object with only the required fields for the stored procedure
                var parameters = new
                {
                    SpecialityName = entity.SpecialityName,
                    Description = entity.Description,
                    TenantID = entity.TenantID
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("Speciality_Insert", parameters, "@SpecialityID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, SpecialityResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, SpecialityResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing Speciality
        public async Task<Response<int>> Update(Entities.EntityClass.Speciality entity)
        {
            var response = new Response<int>();
            try
            {
                // Create parameter object with only the required fields for the stored procedure
                var parameters = new
                {
                    SpecialityID = entity.SpecialityID,
                    SpecialityName = entity.SpecialityName,
                    Description = entity.Description,
                    TenantID = entity.TenantID
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("Speciality_Update", parameters, "@UpdatedSpecialityID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, SpecialityResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, SpecialityResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

