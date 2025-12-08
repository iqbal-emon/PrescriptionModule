using Dapper;
using DataAccess.DatabaseAccessLayer;
using Medication.Domain.Repositories.MedicationManufacturer;
using Medication.Dtos.ReponseDto.MedicationManufacturerDto;
using Medication.Dtos.ResponseDto.MedicationDto;
using Medication.Utility;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Utility;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Medication.Insfracture.RepositoriesImplement.MedicationManufacturer
{
    public class MedicationManufacturerQueryRepository : IMedicationManufacturerQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        private readonly string _connectionString = AppSettings.ConnectionStringForDapper;

        public MedicationManufacturerQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<PagedWithResponse<List<MedicationManufacturerApiReponseDto>>> GetAllManufacturerMostUsed(
      int pageNumber,
      int pageSize,
      string? searchTerm = null,
      string? manufacturerName = null,
      string? days = null)
        {
            var response = new PagedWithResponse<List<MedicationManufacturerApiReponseDto>>();

            try
            {
                using IDbConnection connection = new SqlConnection(_connectionString);

                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);

                // FIX: Pass C# null (or string value) directly. Dapper handles the SQL NULL conversion.
                parameters.Add("@SearchTerm", searchTerm);
                parameters.Add("@CompanyName", manufacturerName);
                parameters.Add("@DateFilter", days);

                // Output parameter remains correct
                parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Execute and map data
                var medications = (await connection.QueryAsync<MedicationManufacturerApiReponseDto>(
                    "MedicationManufacturer_GetMostUsed_Combined",
                    parameters,
                    commandType: CommandType.StoredProcedure)).ToList();

                // Read the TotalCount from the output parameter
                int totalCount = parameters.Get<int>("@TotalCount");

                // Populate Response
                response.TotalCount = totalCount;
                response.Result = medications;
                response.IsSuccess = true;
                response.Message = "Data retrieved successfully.";
            }
            catch (Exception ex)
            {
                // ... (Error handling)
            }

            return response;
        }


        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetAll(string? manufacturerName)
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.Medication>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<
                    Entities.EntityClass.MedicineEntity.Medication,
                    dynamic>(
                        "Medication_GetManufacturers",
                        new { ManufacturerName = manufacturerName } // pass parameter here
                );

                response.Result = result.ToList();
                response.IsSuccess = true;

                ResponseHelper.SetSuccessResponse(
                    response,
                    result,
                    MedicationManufacturerReponseMessage.common_get_all_success,
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

        public Task<Response<List<Entities.EntityClass.MedicineEntity.Medication>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Entities.EntityClass.MedicineEntity.MedicationManufacturer>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.MedicineEntity.MedicationManufacturer>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.MedicineEntity.MedicationManufacturer, dynamic>("MedicationManufacturer_GetById", new
                {
                    ManufacturerId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationManufacturerReponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        Task<Response<Entities.EntityClass.MedicineEntity.Medication>> IBaseCommonQueryMethodRepository<Entities.EntityClass.MedicineEntity.Medication>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
