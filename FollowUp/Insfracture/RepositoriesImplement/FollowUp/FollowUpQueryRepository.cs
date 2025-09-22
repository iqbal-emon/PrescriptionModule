using DataAccess.DatabaseAccessLayer;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;
using Utility.BaseInterface;
using FollowUp.Domain.Repositories.FollowUp;
using FollowUp.Utility;

namespace FollowUp.Insfracture.RepositoriesImplement.FollowUp
{
    public class FollowUpQueryRepository : IFollowUpQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public FollowUpQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.FollowUp>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.FollowUp>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.FollowUp, dynamic>("FollowUp_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, FollowUpResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }
        public async Task<Response<List<Entities.EntityClass.FollowUp>>> GetBookMarks()
        {
            var response = new Response<List<Entities.EntityClass.FollowUp>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.FollowUp, dynamic>("FollowUp_BookMarks", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, FollowUpResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        
        public async Task<Response<List<Entities.EntityClass.FollowUp>>> GetAllFollowUpByName(string FollowUpName)
        {
            var response = new Response<List<Entities.EntityClass.FollowUp>>();

            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.FollowUp, dynamic>("FollowUp_GetFollowUpsByName", new
                {
                    FollowUpName = FollowUpName
                });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, FollowUpResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.FollowUp>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.FollowUp>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.FollowUp, dynamic>("FollowUp_GetById", new
                {
                    FollowUpId = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, FollowUpResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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
