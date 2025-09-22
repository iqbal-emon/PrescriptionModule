using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Prescription.Domain.Repositories.PrescriptionAdvice;
using Prescription.Dtos.ResponseDto.PrescriptionAdvice;
using Prescription.Utility;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Prescription.Insfracture.RepositoriesImplement.PrescriptionAdvice
{
    public class PrescriptionAdviceQueryRepository : IPrescriptionAdviceQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public PrescriptionAdviceQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice, dynamic>("PrescriptionAdvice_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionAdviceResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.PrescriptionEntity.PrescriptionAdvice, dynamic>("PrescriptionAdvice_GetById", new
                {
                    AdviceId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionAdviceResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
        public async Task<Response<List<PrescriptionAdviceResponseDto>>> GetByPrescriptionId(int id)
        {
            var response = new Response<List<PrescriptionAdviceResponseDto>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<PrescriptionAdviceResponseDto, dynamic>("PrescriptionAdvice_GetByPrescriptionId", new
                {
                    PrescriptionId = id
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, PrescriptionAdviceResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
