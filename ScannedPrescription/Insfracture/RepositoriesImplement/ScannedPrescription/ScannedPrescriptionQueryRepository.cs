using DataAccess.DatabaseAccessLayer;
using ScannedPrescription.Utility;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using ScannedPrescription.Domain.Repositories.ScannedPrescription;

namespace DoctorPrescription.Insfracture.RepositoriesImplement.ScannedPrescription
{
    public class ScannedPrescriptionQueryRepository : IScannedPrescrptionQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public ScannedPrescriptionQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.ScannedPrescription, dynamic>("ScannedPrescription_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ScannedPrescriptionResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.ScannedPrescription>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.ScannedPrescription, dynamic>("ScannedPrescription_GetById", new
                {
                    ScannedPrescriptionId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, ScannedPrescriptionResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
