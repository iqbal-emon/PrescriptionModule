using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using MedicationBrand.Domain.Repositories.MedicationBrand;
using MedicationBrand.Utiltiy;

namespace Medication.Infrastructure.RepositoriesImplement.MedicationBrand
{
    public class MedicationBrandCommandRepository : IMedicationBrandCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public MedicationBrandCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.MedicineEntity.MedicationBrand, dynamic>("MedicationBrand_DeleteById", new
                {
                    MedicationBrandId = id
                });

                ResponseHelper.SetSuccessResponse(response, true, MedicationBrandResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.MedicineEntity.MedicationBrand entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.MedicineEntity.MedicationBrand>("MedicationBrand_Insert", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, MedicationBrandResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, MedicationBrandResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);

                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.MedicineEntity.MedicationBrand entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.MedicineEntity.MedicationBrand>("MedicationBrand_Update", entity);
                if (result == 0)
                {
                    ResponseHelper.SetFailedResponse(response, 0, MedicationBrandResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, MedicationBrandResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
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