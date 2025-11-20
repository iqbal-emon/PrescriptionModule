using Dapper;
using DataAccess.DatabaseAccessLayer;
using Entities.EntityClass.PatientEntity;
using Microsoft.AspNetCore.Http;
using PatienFolowUp.Domain.Repositories.Patients;
using PatienFolowUp.Dtos.ResponseDto.Patients;
using PatienFolowUp.Utility;
using Patients.Dtos.ResponseDto.PatientsDto;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace PatienFolowUp.Insfracture.RepositoriesImplement.Patients
{
    public class PatientsQueryRepository : IPatientsQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PatientsQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedWithResponse<List<PatientDataDto>>> GetAll(
      int pageNumber = 1,
      int pageSize = 10,
      string searchTerm = "",
      int? doctorId = null)
        {
            var response = new PagedWithResponse<List<PatientDataDto>>();

            try
            {
                // 1️⃣ TotalCount SP
                var totalCount = await _dataAccess.LoadSingleDataUsingProcedure<int, dynamic>(
                    "Patients_GetTotalCount",
                    new { SearchTerm = searchTerm, DoctorID = doctorId }
                );
                response.TotalCount = totalCount;

                // 2️⃣ PagedData SP
                var pagedData = await _dataAccess.LoadDataUsingProcedure<PatientDataDto, dynamic>(
                    "Patients_GetAll",
                    new
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        SearchTerm = searchTerm,
                        DoctorID = doctorId
                    }
                );

                response.Result = pagedData.ToList();
                response.IsSuccess = true;

                ResponseHelper.SetSuccessResponse(
         response,                  // apiResponse
         totalCount,                // totalCount
         pagedData,            // result
         PatientsResponseMessage.common_get_all_success,
         StatusResponseMessage.success,
         StatusCodes.Status200OK
     );
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the patients.";
                ResponseHelper.SetFailedResponse(
                    response,
                    0,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(
                    response,
                    0,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status500InternalServerError
                );
            }

            return response;
        }


        public async Task<Response<Patient>> GetByRoleAndReferenceId(int userId)
        {
            var response = new Response<Patient>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Patient, dynamic>("Patients_GetByUserId", new
                {
                    UserId=userId,
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PatientsResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }


        public async Task<Response<PatientsApiResponseDto>> GetById(int id)
        {
            var response = new Response<PatientsApiResponseDto>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<PatientsApiResponseDto, dynamic>("Patients_GetById", new
                {
                    PatientID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PatientsResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }


        public async Task<Response<PatientsApiResponseDto>> GetByPhoneNo(string phoneNo)
        {
            var response = new Response<PatientsApiResponseDto>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<PatientsApiResponseDto, dynamic>(
                    "Patients_GetByPhoneNo",
                    new
                    {
                        PhoneNo = phoneNo  // Correct
                    }
                );

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PatientsResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }


        public async Task<Response<List<PatientAgeDistributionResponseDto>>> GetAgeDistribution()
        {
            var response = new Response<List<PatientAgeDistributionResponseDto>>();

            try
            {
                // Load data from stored procedure
                var result = await _dataAccess.LoadDataUsingProcedure<PatientAgeDistributionResponseDto, dynamic>(
                    "Patients_GetAgeDistribution", new { });

                // Calculate total and percentages
                var total = result.Sum(r => r.PatientCount);

                foreach (var item in result)
                {
                    item.Percentage = total > 0
                        ? Math.Round((item.PatientCount / (double)total) * 100, 2)
                        : 0;
                }

                // ✅ Set success response
                ResponseHelper.SetSuccessResponse(
                    response,
                    result,
                    "Age distribution fetched successfully.",
                    StatusResponseMessage.success,
                    StatusCodes.Status200OK
                );
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(
                    response,
                    null,
                    response.Message,
                    StatusResponseMessage.failed,
                    StatusCodes.Status400BadRequest
                );
            }

            return response;
        }

        public Task<Response<List<Patient>>> GetAll()
        {
            throw new NotImplementedException();
        }

        Task<Response<Patient>> IBaseCommonQueryMethodRepository<Patient>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}