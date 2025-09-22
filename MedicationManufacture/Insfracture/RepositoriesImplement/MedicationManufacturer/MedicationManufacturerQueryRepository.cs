using DataAccess.DatabaseAccessLayer;
using MedicationManufacturer.Domain.Repositories.MedicationManufacturer;
using MedicationManufacturer.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace MedicationManufacturer.Insfracture.RepositoriesImplement.MedicationManufacturer
{
    public class MedicationManufacturerQueryRepository : IMedicationManufacturerQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public MedicationManufacturerQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.MedicineEntity.MedicationManufacturer>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.MedicineEntity.MedicationManufacturer>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.MedicineEntity.MedicationManufacturer, dynamic>("MedicationManufacturer_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, MedicationManufacturerReponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
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
    }
}
