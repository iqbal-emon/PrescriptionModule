using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.DoctorDegree;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorDegree
{
    public class DoctorDegreeCommandRepository : IDoctorDegreeCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorDegreeCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorDegreeDeleteModel
                {
                    DoctorDegreeID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorDegree, DoctorDegreeDeleteModel>("DoctorDegree_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorDegreeResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorDegree entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorDegreeInsertModel
                {
                    DoctorDegreeID = entity.DoctorDegreeID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    DegreeID = entity.DegreeID,
                    PassingYear = entity.PassingYear,
                    InstituteName = entity.InstituteName,
                    InstituteID = entity.InstituteID,
                    Country = entity.Country,
                    CountryID = entity.CountryID,
                    City = entity.City,
                    CityID = entity.CityID,
                    ZipCode = entity.ZipCode,
                    ZipCodeID = entity.ZipCodeID,
                    IsDeleted = entity.IsDeleted,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorDegreeInsertModel>("DoctorDegree_Insert", insertModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorDegreeResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorDegreeResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorDegree entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorDegreeUpdateModel
                {
                    DoctorDegreeID = entity.DoctorDegreeID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    DegreeID = entity.DegreeID,
                    PassingYear = entity.PassingYear,
                    InstituteName = entity.InstituteName,
                    InstituteID = entity.InstituteID,
                    Country = entity.Country,
                    CountryID = entity.CountryID,
                    City = entity.City,
                    CityID = entity.CityID,
                    ZipCode = entity.ZipCode,
                    ZipCodeID = entity.ZipCodeID,
                    IsDeleted = entity.IsDeleted,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorDegreeUpdateModel>("DoctorDegree_Update", updateModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorDegreeResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorDegreeResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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

