using Advice.Domain.Repositories.AdviceTranslations;
using Advice.Utility;
using DataAccess.DatabaseAccessLayer;

using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace Advice.Insfracture.RepositoriesImplement.AdviceTranslation
{
    public class AdviceTranslationsQueryRepository : IAdviceTranslationsQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public AdviceTranslationsQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.AdviceTranslation>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.AdviceTranslation>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.AdviceTranslation, dynamic>("AdviceTranslations_GetAll", new
                {

                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AdviceTranslationsResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.AdviceTranslation>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.AdviceTranslation>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.AdviceTranslation, dynamic>("AdviceTranslations_GetById", new
                {
                    AdviceTranslationsId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, AdviceTranslationsResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
