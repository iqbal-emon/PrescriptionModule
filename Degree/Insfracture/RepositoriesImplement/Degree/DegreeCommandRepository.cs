using DataAccess.DatabaseAccessLayer;
using Degree.DatabaseModels;
using Degree.Domain.Repositories.Degree;
using Degree.Utility;
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

namespace Degree.Insfracture.RepositoriesImplement.Degree
{

    public class DegreeCommandRepository : IDegreeCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DegreeCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Delete a Prescription Diagnosis by ID
        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new DegreeDeleteModel
                {
                    DegreeID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<DegreeDeleteModel, DegreeDeleteModel>(
                    "Degree_DeleteById", 
                    deleteModel
                );

                ResponseHelper.SetSuccessResponse(response, true, DegreeResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Insert a new Prescription Diagnosis
        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.Degree entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new DegreeInsertModel
                {
                    DegreeID = 0, // SP accepts but doesn't use (auto-generated)
                    TenantId = entity.TenantID,
                    DegreeName = entity.DegreeName,
                    Duration = entity.Duration,
                    DurationType = entity.DurationType,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DegreeInsertModel>(
                    "Degree_Insert", 
                    insertModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DegreeResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DegreeResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
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
        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.Degree entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new DegreeUpdateModel
                {
                    DegreeID = entity.DegreeID,
                    TenantId = entity.TenantID > 0 ? entity.TenantID : null,
                    DegreeName = entity.DegreeName,
                    Duration = entity.Duration,
                    DurationType = entity.DurationType,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DegreeUpdateModel>(
                    "Degree_Update", 
                    updateModel
                );
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DegreeResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DegreeResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
