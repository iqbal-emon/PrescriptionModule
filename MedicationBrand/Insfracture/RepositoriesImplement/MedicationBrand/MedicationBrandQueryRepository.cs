using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using MedicationBrand.Domain.Repositories.MedicationBrand;
using MedicationBrand.Utiltiy;

namespace Medication.Insfracture.RepositoriesImplement.MedicationBrand
{
    public class MedicationBrandQueryRepository : IMedicationBrandQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public MedicationBrandQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.MedicineEntity.MedicationBrand>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.MedicationBrand>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.MedicineEntity.MedicationBrand, dynamic>("MedicationBrand_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationBrandResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.MedicineEntity.MedicationBrand>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.MedicineEntity.MedicationBrand>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.MedicineEntity.MedicationBrand, dynamic>("MedicationBrand_GetById", new
                {
                    MedicationBrandId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationBrandResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
