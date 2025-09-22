using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using ExpertiseCategory.Domain.Repositories.ExpertiseCategory;

namespace ExpertiseCategory.Insfracture.RepositoriesImplement.ExpertiseCategory
{
    public class ExpertiseCategoryCommandRepository : IExpertiseCategoryCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public ExpertiseCategoryCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete an Expertise Category by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.ExpertiseCategory, dynamic>("ExpertiseCategory_DeleteById", new
                {
                    ExpertiseID = id
                });

                ResponseHelper.SetSuccessResponse(response, true, ExpertiseCategoryResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Insert a new Expertise Category
        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.ExpertiseCategory entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("ExpertiseCategory_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, ExpertiseCategoryResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, ExpertiseCategoryResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        // Update an existing Expertise Category
        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.ExpertiseCategory entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType("ExpertiseCategory_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, ExpertiseCategoryResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, ExpertiseCategoryResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
