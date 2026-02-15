using DataAccess.DatabaseAccessLayer;
using Doctor.Utility;
using Doctor.Domain.Repositories.DoctorChamber;
using Doctor.DatabaseModels;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.DoctorChamber
{
    public class DoctorChamberCommandRepository : IDoctorChamberCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public DoctorChamberCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorChamberDeleteModel
                {
                    ChamberID = id
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorChamber, DoctorChamberDeleteModel>("DoctorChamber_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorChamberResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.DoctorEntity.DoctorChamber entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorChamberInsertModel
                {
                    ChamberID = entity.ChamberID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    ChamberName = entity.ChamberName,
                    Address = entity.Address,
                    Country = entity.Country,
                    CountryID = entity.CountryID,
                    City = entity.City,
                    CityID = entity.CityID,
                    ZipCode = entity.ZipCode,
                    ZipCodeID = entity.ZipCodeID,
                    IsVisibleOnPrescription = entity.IsVisibleOnPrescription,
                    ChamberReferenceId = entity.ChamberReferenceId,
                    DistrictId = entity.DistrictId,
                    DivisionId = entity.DivisionId,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorChamberInsertModel>("DoctorChamber_Insert", insertModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, DoctorChamberResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorChamberResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.DoctorEntity.DoctorChamber entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorChamberUpdateModel
                {
                    ChamberID = entity.ChamberID,
                    TenantID = entity.TenantID,
                    DoctorID = entity.DoctorID,
                    ChamberName = entity.ChamberName,
                    Address = entity.Address,
                    Country = entity.Country,
                    CountryID = entity.CountryID,
                    City = entity.City,
                    CityID = entity.CityID,
                    ZipCode = entity.ZipCode,
                    ZipCodeID = entity.ZipCodeID,
                    IsVisibleOnPrescription = entity.IsVisibleOnPrescription,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted,
                    CreatedAt = entity.CreatedAt
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorChamberUpdateModel>("DoctorChamber_Update", updateModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorChamberResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorChamberResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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
