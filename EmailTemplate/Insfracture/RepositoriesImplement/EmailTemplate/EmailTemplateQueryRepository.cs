using DataAccess.DatabaseAccessLayer;
using EmailTemplate.Domain.Repositories.EmailTemplate;
using EmailTemplate.Utility;
using Medication.Utility;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace EmailTemplate.Insfracture.RepositoriesImplement.EmailTemplate
{
    public class EmailTemplateQueryRepository : IEmailTemplateQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public EmailTemplateQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.EmailTemplate>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.EmailTemplate>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.EmailTemplate, dynamic>("EmailTemplate_GetAll", new { });

                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, EmailTemplateResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public async Task<Response<Entities.EntityClass.EmailTemplate>> GetById(Guid id)
        {
            var response = new Response<Entities.EntityClass.EmailTemplate>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.EmailTemplate, dynamic>("EmailTemplate_GetById", new
                {
                    EmailTemplateId = id
                });

                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, EmailTemplateResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public Task<Response<Entities.EntityClass.EmailTemplate>> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }

       
    }
