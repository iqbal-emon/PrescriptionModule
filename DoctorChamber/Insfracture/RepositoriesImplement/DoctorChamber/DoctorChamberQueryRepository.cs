using DataAccess.DatabaseAccessLayer;
using DoctorChamber.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;
using DoctorChamber.Domain.Repositories.DoctorChamber;

namespace DoctorChamber.Insfracture.RepositoriesImplement.DoctorChamber
{
    public class DoctorChamberQueryRepository : IDoctorChamberQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DoctorChamberQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all Doctor Chambers
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorChamber, dynamic>("DoctorChamber_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorChamberResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Doctor Chamber by ID
        public async Task<Response<Entities.EntityClass.DoctorEntity.DoctorChamber>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DoctorEntity.DoctorChamber>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorChamber, dynamic>("DoctorChamber_GetById", new
                {
                    ChamberID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorChamberResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Doctor Chambers by Name
        public async Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>> GetAllChamberName(string chamberName)
        {
            var response = new Response<List<Entities.EntityClass.DoctorEntity.DoctorChamber>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DoctorEntity.DoctorChamber, dynamic>("DoctorChamber_GetChambersByName", new
                {
                    ChamberName = chamberName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DoctorChamberResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }
        public async Task<ApiResponse<List<Entities.CountryEntity.District>>> GetAllDistrict(int divisonId)
        {
            var apiResponse = new ApiResponse<List<Entities.CountryEntity.District>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<
                    Entities.CountryEntity.District,
                    dynamic
                >("District_GetAll", new {
                    DivisionId = divisonId       });

                apiResponse.Results = result.ToList();

                ApiResponseHelper.SetSuccessResponse(
                    apiResponse,
                    apiResponse.Results,
                    "All districts retrieved successfully",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(
                    apiResponse,
                    null,
                    StandardDataAccessMessages.GetSqlErrorMessage(ex)
                );
            }

            return apiResponse;
        }


        public async Task<ApiResponse<List<Entities.CountryEntity.Division>>> GetAllDivision()
        {
            var apiResponse = new ApiResponse<List<Entities.CountryEntity.Division>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<
                    Entities.CountryEntity.Division,
                    dynamic
                >("Division_GetAll", new { });

                apiResponse.Results = result.ToList();

                ApiResponseHelper.SetSuccessResponse(
                    apiResponse,
                    apiResponse.Results,
                    "All districts retrieved successfully",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(
                    apiResponse,
                    null,
                    StandardDataAccessMessages.GetSqlErrorMessage(ex)
                );
            }

            return apiResponse;
        }




    }
}
