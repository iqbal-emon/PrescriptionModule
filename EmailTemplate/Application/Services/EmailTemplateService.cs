using EmailTemplate.Domain.Repositories;
using EmailTemplate.Domain.Repositories.EmailTemplate;
using EmailTemplate.Dtos.RequestDto;
using EmailTemplate.Dtos.RequestDto.EmailTemplateDto;
using EmailTemplate.Dtos.RquestDto.EmailTemplateDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System.Data.SqlClient;
using Utility.ApiResponse;
using Utility.Response;

namespace EmailTemplate.Application.Services
{
    public class EmailTemplateService
    {
        private readonly IEmailTemplateQueryRepository _emailTemplateQueryRepository;
        private readonly IEmailTemplateCommandRepository _emailTemplateCommandRepository;
        private readonly MapperService _mapperService;

        public EmailTemplateService(IEmailTemplateQueryRepository emailTemplateQueryRepository, IEmailTemplateCommandRepository emailTemplateCommandRepository,
            MapperService mapperService)
        {
            _emailTemplateQueryRepository = emailTemplateQueryRepository;
            _emailTemplateCommandRepository = emailTemplateCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.EmailTemplate>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.EmailTemplate>>();

            try
            {
                var templates = await _emailTemplateQueryRepository.GetAll();

                if (templates == null)
                {
                    ResponseHelper.SetFailedResponse(response, templates.Result, templates.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, templates.Result, templates.Message, StatusResponseMessage.success, templates.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving email templates.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.EmailTemplate>> GetById(Guid id)
        {
            var response = new Response<Entities.EntityClass.EmailTemplate>();

            try
            {
                var template = await _emailTemplateQueryRepository.GetById(id);

                if (template == null)
                {
                    ResponseHelper.SetFailedResponse(response, template.Result, template.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, template.Result, template.Message, StatusResponseMessage.success, template.StatusCode);
                }
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while retrieving the email template.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Guid>> Insert(EmailTemplateInsertRequestDto templateDto)
        {
            var response = new Response<Guid>();

            try
            {
                var templateEntity = await _mapperService.MapSingle<EmailTemplateInsertRequestDto, Entities.EntityClass.EmailTemplate>(templateDto);
                var insertResponse = await _emailTemplateCommandRepository.Insert(templateEntity);
                response = insertResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while inserting the email template.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while inserting the email template.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Guid>> Update(EmailTemplateUpdateRequestDto templateDto)
        {
            var response = new Response<Guid>();

            try
            {
                var templateEntity = await _mapperService.MapSingle<EmailTemplateUpdateRequestDto, Entities.EntityClass.EmailTemplate>(templateDto);
                var updatedResponse = await _emailTemplateCommandRepository.Update(templateEntity);
                response = updatedResponse;
            }
            catch (SqlException)
            {
                response.Message = "A database error occurred while updating the email template.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                response.Message = "An unexpected error occurred while updating the email template.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.failed, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(Guid id)
        {
            return _emailTemplateCommandRepository.Delete(id);
        }
    }
}