using DataAccess.DatabaseAccessLayer;
using Doctor.Domain.Repositories.Doctor;
using Doctor.DatabaseModels;
using Doctor.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Doctor.Insfracture.RepositoriesImplement.Doctor
{
    public class DoctorCommandRepository : IDoctorCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DoctorCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int doctorId)
        {
            var response = new Response<bool>();
            try
            {
                var deleteModel = new DoctorDeleteModel
                {
                    DoctorID = doctorId
                };
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Doctor, DoctorDeleteModel>("Doctor_DeleteById", deleteModel);

                ResponseHelper.SetSuccessResponse(response, true, DoctorResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Doctor entity)
        {
            var response = new Response<int>();
            try
            {
                var insertModel = new DoctorInsertModel
                {
                    UserID = entity.UserID,
                    DoctorID = entity.DoctorID,
                    SpecialityID = entity.SpecialityID,
                    Specialization = entity.Specialization,
                    LicenseNumber = entity.LicenseNumber,
                    DoctorReferenceID = entity.DoctorReferenceID,
                    HospitalAffiliation = entity.HospitalAffiliation,
                    Expertise = entity.Expertise,
                    ProfileStep = entity.ProfileStep,
                    BmdcRegNo = entity.BmdcRegNo,
                    BmdcRegExpiryDate = entity.BmdcRegExpiryDate,
                    IdentityNumber = entity.IdentityNumber,
                    City = entity.City,
                    Country = entity.Country,
                    Address = entity.Address,
                    DoctorTitle = entity.DoctorTitle,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorInsertModel>("Doctor_Insert", insertModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Doctor entity)
        {
            var response = new Response<int>();
            try
            {
                var updateModel = new DoctorUpdateModel
                {
                    DoctorID = entity.DoctorID,
                    UserID = entity.UserID,
                    SpecialityID = entity.SpecialityID,
                    Specialization = entity.Specialization,
                    LicenseNumber = entity.LicenseNumber,
                    DoctorReferenceID = entity.DoctorReferenceID,
                    HospitalAffiliation = entity.HospitalAffiliation,
                    Expertise = entity.Expertise,
                    ProfileStep = entity.ProfileStep,
                    BmdcRegNo = entity.BmdcRegNo,
                    BmdcRegExpiryDate = entity.BmdcRegExpiryDate,
                    IdentityNumber = entity.IdentityNumber,
                    City = entity.City,
                    Country = entity.Country,
                    Address = entity.Address,
                    DoctorTitle = entity.DoctorTitle,
                    UpdatedAt = entity.UpdatedAt,
                    IsDeleted = entity.IsDeleted,
                    CreatedAt = entity.CreatedAt
                };
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<DoctorUpdateModel>("Doctor_Update", updateModel);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, result, DoctorResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, DoctorResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<bool>> UpdateActiveStatus(int doctorId, bool isActive)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Doctor, dynamic>("Doctor_UpdateActiveStatus", new
                {
                    DoctorID = doctorId,
                    IsActive = isActive
                });

                ResponseHelper.SetSuccessResponse(response, true, DoctorResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<bool>> UpdateOnlineStatus(int doctorId, bool isOnline)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Doctor, dynamic>("Doctor_UpdateOnlineStatus", new
                {
                    DoctorID = doctorId,
                    IsOnline = isOnline
                });

                ResponseHelper.SetSuccessResponse(response, true, DoctorResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<bool>> UpdateExpertise(int doctorId, string expertise)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Doctor, dynamic>("Doctor_UpdateExpertise", new
                {
                    DoctorID = doctorId,
                    Expertise = expertise
                });

                ResponseHelper.SetSuccessResponse(response, true, DoctorResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<bool>> UpdateProfileStep(int doctorId, int profileStep)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Doctor, dynamic>("Doctor_UpdateProfileStep", new
                {
                    DoctorID = doctorId,
                    ProfileStep = profileStep
                });

                ResponseHelper.SetSuccessResponse(response, true, DoctorResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
    }
}
