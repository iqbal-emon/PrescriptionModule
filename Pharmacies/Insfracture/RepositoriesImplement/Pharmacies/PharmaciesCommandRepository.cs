using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Pharmacies.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using System;
using System.Threading.Tasks;
using Pharmacies.Domain.Repositories.Pharmacies;

namespace Pharmacies.Insfracture.RepositoriesImplement.Pharmacy
{
    public class PharmaciesCommandRepository : IPharmaciesCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PharmaciesCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(int id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Pharmacy, dynamic>("Pharmacy_DeleteById", new
                {
                    PharmacyId = id
                });

                ResponseHelper.SetSuccessResponse(response, true, PharmaciesResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<int>> Insert(Entities.EntityClass.Pharmacy entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.Pharmacy>("Pharmacy_Insert", entity);
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PharmaciesResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PharmaciesResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);

                }
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, 0, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<int>> Update(Entities.EntityClass.Pharmacy entity)
        {
            var response = new Response<int>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.Pharmacy>("Pharmacy_Update", entity);
                if (result == 0)
                {

                    ResponseHelper.SetFailedResponse(response, result, PharmaciesResponseMessage.common_update_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                    ResponseHelper.SetSuccessResponse(response, result, PharmaciesResponseMessage.common_update_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);


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
