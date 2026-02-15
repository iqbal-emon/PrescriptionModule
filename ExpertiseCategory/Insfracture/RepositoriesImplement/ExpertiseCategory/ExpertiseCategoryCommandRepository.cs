using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Utility;
using Entities.EntityClass;
using ExpertiseCategory.DatabaseModels;
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
                // Create database model matching stored procedure parameters
                var deleteModel = new ExpertiseCategoryDeleteModel
                {
                    ExpertiseID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<ExpertiseCategoryDeleteModel, ExpertiseCategoryDeleteModel>(
                    "ExpertiseCategory_DeleteById", 
                    deleteModel
                );

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
                // Map entity to database model matching stored procedure parameters
                var insertModel = new ExpertiseCategoryInsertModel
                {
                    TenantID = entity.TenantID,
                    ExpertiseName = entity.ExpertiseName,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<ExpertiseCategoryInsertModel>(
                    "ExpertiseCategory_Insert", 
                    insertModel
                );
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
                // Map entity to database model matching stored procedure parameters
                var updateModel = new ExpertiseCategoryUpdateModel
                {
                    ExpertiseID = entity.ExpertiseID,
                    TenantID = entity.TenantID > 0 ? entity.TenantID : null,
                    ExpertiseName = entity.ExpertiseName,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<ExpertiseCategoryUpdateModel>(
                    "ExpertiseCategory_Update", 
                    updateModel
                );
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
