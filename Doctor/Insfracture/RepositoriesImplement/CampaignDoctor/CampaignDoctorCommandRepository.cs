using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.CampaignDoctor;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.CampaignDoctor
{
    public class CampaignDoctorCommandRepository : ICampaignDoctorCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public CampaignDoctorCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new CampaignDoctorDeleteModel
                {
                    CampaignDoctorID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.CampaignDoctor, CampaignDoctorDeleteModel>("CampaignDoctor_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, CampaignDoctorResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.CampaignDoctor entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new CampaignDoctorInsertModel
                {
                    DoctorID = entity.DoctorID,
                    CampaignID = entity.CampaignID
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("CampaignDoctor_Insert", insertModel, "@CampaignDoctorID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, CampaignDoctorResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, CampaignDoctorResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.CampaignDoctor entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new CampaignDoctorUpdateModel
                {
                    CampaignDoctorID = entity.CampaignDoctorID,
                    DoctorID = entity.DoctorID,
                    CampaignID = entity.CampaignID
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("CampaignDoctor_Update", updateModel, "@UpdatedId");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, CampaignDoctorResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, CampaignDoctorResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

