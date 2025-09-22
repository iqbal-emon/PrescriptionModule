using DataAccess.DatabaseAccessLayer;
using EmailTemplate.Domain.Repositories.EmailTemplate;
using EmailTemplate.Utility;
using Medication.Utility;
using Microsoft.AspNetCore.Http;
using Utility.ApiResponse;
using Utility.BaseInterface;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace EmailTemplate.Insfracture.RepositoriesImplement.EmailTemplate
{
    public class EmailTemplateCommandRepository : IEmailTemplateCommandRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;

        public EmailTemplateCommandRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.EmailTemplate, dynamic>("EmailTemplate_DeleteById", new
                {
                    EmailTemplateId = id
                });

                ResponseHelper.SetSuccessResponse(response, true, EmailTemplateResponseMessage.common_delete_success_message, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, false, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Guid>> Insert(Entities.EntityClass.EmailTemplate entity)
        {
            var response = new Response<Guid>();
            try
            {
             

                // Calling SaveDataUsingProcedureReturnIdTuple and getting the tuple result
                var result = await _dataAccess.SaveDataUsingProcedureReturnId<Entities.EntityClass.EmailTemplate>("EmailTemplate_Insert", entity);

             

                    if (result == Guid.Empty)
                    {
                        ResponseHelper.SetFailedResponse(response, result, EmailTemplateResponseMessage.common_inserted_failed_message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                    }
                    else
                    {
                        response.Result = result;
                        response.IsSuccess = true;
                        ResponseHelper.SetSuccessResponse(response, result, EmailTemplateResponseMessage.common_insert_success_message, StatusResponseMessage.success, StatusCodes.Status201Created);
                    }
               
               
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, Guid.Empty, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }
        

        public async Task<Response<Guid>> Update(Entities.EntityClass.EmailTemplate entity)
        {
            var response = new Response<Guid>();
            try
            {
                var result = await _dataAccess.SaveDataUsingProcedureReturnId<Entities.EntityClass.EmailTemplate>("EmailTemplate_Update", entity);
                
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, Guid.Empty, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        Task<Response<int>> IBaseCommonCommandMethodRepository<Entities.EntityClass.EmailTemplate>.Insert(Entities.EntityClass.EmailTemplate entity)
        {
            throw new NotImplementedException();
        }

        Task<Response<int>> IBaseCommonCommandMethodRepository<Entities.EntityClass.EmailTemplate>.Update(Entities.EntityClass.EmailTemplate entity)
        {
            throw new NotImplementedException();
        }
    }
}
