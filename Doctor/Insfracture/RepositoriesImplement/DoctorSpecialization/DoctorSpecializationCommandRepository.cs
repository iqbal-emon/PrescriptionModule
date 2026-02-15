using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.DoctorSpecialization;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorSpecialization
{
    public class DoctorSpecializationCommandRepository : IDoctorSpecializationCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorSpecializationCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorSpecializationDeleteModel
                {
                    DoctorSpecializationID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorSpecialization, DoctorSpecializationDeleteModel>("DoctorSpecialization_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorSpecializationResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorSpecialization entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorSpecializationInsertModel
                {
                    DoctorID = entity.DoctorID,
                    SpecialityID = entity.SpecialityID,
                    SpecializationID = entity.SpecializationID,
                    ServiceDetails = entity.ServiceDetails,
                    DocumentName = entity.DocumentName,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorSpecialization_Insert", insertModel, "@DoctorSpecializationID");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorSpecializationResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorSpecializationResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorSpecialization entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorSpecializationUpdateModel
                {
                    DoctorSpecializationID = entity.DoctorSpecializationID,
                    DoctorID = entity.DoctorID,
                    SpecialityID = entity.SpecialityID,
                    SpecializationID = entity.SpecializationID,
                    ServiceDetails = entity.ServiceDetails,
                    DocumentName = entity.DocumentName
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIntIdWithCustomOutput("DoctorSpecialization_Update", updateModel, "@UpdatedId");
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorSpecializationResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorSpecializationResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

