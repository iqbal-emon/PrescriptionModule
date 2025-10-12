using DataAccess.DatabaseAccessLayer;
using Medication.Domain.Repositories.Medication;
using Medication.Dtos.ResponseDto.MedicationDto;
using Medication.Utility;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Medication.Insfracture.RepositoriesImplement.Medication
{
    public class MedicationQueryRepository : IMedicationQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public MedicationQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.Medication>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.MedicineEntity.Medication, dynamic>("Medication_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<PagedWithResponse<List<MedicationMostUsedDto>>> GetAllMedicineMostUsed(
     int pageNumber,
     int pageSize,
     string? searchTerm = null,
     string? manufacturerName = null, string? days = null)
        {
            var response = new PagedWithResponse<List<MedicationMostUsedDto>>();

            var totalCount = await _dataAccess.LoadSingleDataUsingProcedure<int, dynamic>(
                "Medication_GetMostUsed_TotalCount",
                new { SearchTerm = searchTerm, CompanyName = manufacturerName, DateFilter = days }
            );

            var data = await _dataAccess.LoadDataUsingProcedure<MedicationMostUsedDto, dynamic>(
                "Medication_GetMostUsed",
                new { PageNumber = pageNumber, PageSize = pageSize, SearchTerm = searchTerm, CompanyName = manufacturerName, DateFilter = days }
            );

            response.TotalCount = totalCount;
            response.Result = data.ToList();

            return response;
        }




        public async Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetBookMarks(int doctorId)
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.Medication>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.MedicineEntity.Medication, dynamic>("GetBookMarksMedicationByDoctorId", new
                {
                    DoctorReferenceID = doctorId
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }


        public async Task<Response<Entities.EntityClass.MedicineEntity.Medication>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.MedicineEntity.Medication>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.MedicineEntity.Medication, dynamic>("Medication_GetById", new
                {
                    MedicationId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetMedicationByName(string name, string uniCode)
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.Medication>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.MedicineEntity.Medication, dynamic>("GetMedicationByName", new
                {
                    MedicationName = name,
                  
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
    }
}
