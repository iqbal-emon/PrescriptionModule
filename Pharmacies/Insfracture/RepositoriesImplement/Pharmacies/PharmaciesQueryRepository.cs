using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Pharmacies.Domain.Repositories.Pharmacies;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacies.Utility;

namespace Pharmacies.Insfracture.RepositoriesImplement.Pharmacy
{
    public class PharmaciesQueryRepository : IPharmaciesQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public PharmaciesQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.Pharmacy>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.Pharmacy>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.Pharmacy, dynamic>("Pharmacy_GetAll", new { });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PharmaciesResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.Pharmacy>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.Pharmacy>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Pharmacy, dynamic>("Pharmacy_GetById", new
                {
                    PharmacyId = id
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PharmaciesResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
