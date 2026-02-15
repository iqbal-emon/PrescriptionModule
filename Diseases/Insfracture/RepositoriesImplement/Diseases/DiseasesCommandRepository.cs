using DataAccess.DatabaseAccessLayer;
using Diseases.DatabaseModels;
using Diseases.Domain.Repositories.Diseases;
using Diseases.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Diseases.Insfracture.RepositoriesImplement.Diseases
{
    public class DiseasesCommandRepository : IDiseasesCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DiseasesCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Create database model matching stored procedure parameters
                var deleteModel = new DiseaseDeleteModel
                {
                    DiseaseID = id
                };

                var result = await _dataAccess.LoadSingleDataUsingProcedure<DiseaseDeleteModel, DiseaseDeleteModel>(
                    "Disease_DeleteById", 
                    deleteModel
                );


                ResponseHelper.SetSuccessResponse(response, true, DiseasesResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Disease entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var insertModel = new DiseaseInsertModel
                {
                    DiseaseID = 0, // SP accepts but doesn't use (auto-generated)
                    TenantId = entity.TenantId,
                    DiseaseName = entity.DiseaseName,
                    Description = entity.Description,
                    // CreatedAt, UpdatedAt, IsDeleted are optional - SP handles them
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DiseaseInsertModel>(
                    "Disease_Insert", 
                    insertModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, DiseasesResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DiseasesResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);


                }




            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Disease entity)
        {
            var response = new Response<int>();
            try
            {
                // Map entity to database model matching stored procedure parameters
                var updateModel = new DiseaseUpdateModel
                {
                    DiseaseID = entity.DiseaseId,
                    TenantId = entity.TenantId > 0 ? entity.TenantId : null,
                    DiseaseName = entity.DiseaseName,
                    Description = entity.Description,
                    // UpdatedAt is optional - SP uses GETUTCDATE()
                };

                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DiseaseUpdateModel>(
                    "Disease_Update", 
                    updateModel
                );
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, DiseasesResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DiseasesResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
