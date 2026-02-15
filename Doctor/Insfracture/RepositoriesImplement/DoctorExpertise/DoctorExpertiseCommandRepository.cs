using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using Doctor.Domain.Repositories.DoctorExpertise;
using Doctor.Utility;
using Doctor.DatabaseModels;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorExpertise
{
    public class DoctorExpertiseCommandRepository : IDoctorExpertiseCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorExpertiseCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorExpertiseDeleteModel
                {
                    DoctorExpertiseID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorExpertise, DoctorExpertiseDeleteModel>("DoctorExpertise_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorExpertiseResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorExpertise entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorExpertiseInsertModel
                {
                    DoctorExpertiseID = entity.DoctorExpertiseID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    ExpertiseID = entity.ExpertiseID,
                    ExperienceYears = entity.ExperienceYears,
                    Certification = entity.Certification,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorExpertiseInsertModel>("DoctorExpertise_Insert", insertModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorExpertiseResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorExpertiseResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorExpertise entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorExpertiseUpdateModel
                {
                    DoctorExpertiseID = entity.DoctorExpertiseID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    ExpertiseID = entity.ExpertiseID,
                    ExperienceYears = entity.ExperienceYears,
                    Certification = entity.Certification,
                    IsDeleted = entity.IsDeleted,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorExpertiseUpdateModel>("DoctorExpertise_Update", updateModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorExpertiseResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorExpertiseResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

