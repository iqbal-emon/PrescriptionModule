using EmailTemplate.Application.Services;
using EmailTemplate.Dtos.ResponseDto;
using EmailTemplate.Dtos.RequestDto;
using EmailTemplate.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.CommonService;
using SharedService.MapService;
using Utility.ApiResponse;
using Utility.Permission;
using EmailTemplate.Dtos.RquestDto.EmailTemplateDto;
using System.Runtime.InteropServices;
using EmailTemplate.Dtos.RequestDto.EmailTemplateDto;
using EmailTemplate.Dtos.ResponseDto.EmailTemplateDto;

namespace EmailTemplate.Controllers
{
    [ApiController]
    [Route("api/2025-02/")]
    public class EmailTemplateController : ControllerBase
    {
        private readonly SharedCommonService _sharedCommonService;
        private readonly MapperService _mapperService;
        private readonly EmailTemplateService _emailTemplateService;

        public EmailTemplateController(
            SharedCommonService sharedCommonService,
            MapperService mapperService,
            EmailTemplateService emailTemplateService)
        {
            _sharedCommonService = sharedCommonService;
            _mapperService = mapperService;
            _emailTemplateService = emailTemplateService;
        }

        [Authorize(Policy = PermissionConstants.EmailTemplateGetAll)]
        [HttpGet("gets-all-email-templates")]
        public async Task<ActionResult<ApiResponse<List<EmailTemplateApiResponseDto>>>> GetAllEmailTemplates()
        {
            var apiResponse = new ApiResponse<List<EmailTemplateApiResponseDto>>();
            try
            {
                var templates = await _emailTemplateService.GetAll();
                var mappedTemplates = await _mapperService.MapList<Entities.EntityClass.EmailTemplate, EmailTemplateApiResponseDto>(templates.Result);

                if (templates.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, EmailTemplateApiConstantsResponseMessage.email_template_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedTemplates;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, EmailTemplateApiConstantsResponseMessage.email_template_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, EmailTemplateApiConstantsResponseMessage.email_template_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.EmailTemplateGetById)]
        [HttpGet("get-email-template-by-id")]
        public async Task<ActionResult<ApiResponse<EmailTemplateApiResponseDto>>> GetEmailTemplateById(Guid templateId)
        {
            var apiResponse = new ApiResponse<EmailTemplateApiResponseDto>();
            try
            {
                var template = await _emailTemplateService.GetById(templateId);
                var mappedTemplate = await _mapperService.MapSingle<Entities.EntityClass.EmailTemplate, EmailTemplateApiResponseDto>(template.Result);

                if (template.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, EmailTemplateApiConstantsResponseMessage.email_template_null_of_get_list);
                    return Ok(apiResponse);
                }

                apiResponse.Results = mappedTemplate;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, EmailTemplateApiConstantsResponseMessage.email_template_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, EmailTemplateApiConstantsResponseMessage.email_template_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.EmailTemplateCreate)]
        [HttpPost("create-email-template")]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateEmailTemplate(EmailTemplateInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<Guid>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _emailTemplateService.Insert(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, EmailTemplateApiConstantsResponseMessage.email_template_insert_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, Guid.Empty, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, Guid.Empty, EmailTemplateApiConstantsResponseMessage.email_template_inserted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, Guid.Empty, EmailTemplateApiConstantsResponseMessage.email_template_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.EmailTemplateUpdate)]
        [HttpPut("update-email-template")]
        public async Task<ActionResult<ApiResponse<Guid>>> UpdateEmailTemplate(EmailTemplateUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<Guid>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _emailTemplateService.Update(request);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, EmailTemplateApiConstantsResponseMessage.email_template_update_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, Guid.Empty, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, Guid.Empty, EmailTemplateApiConstantsResponseMessage.email_template_update_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, Guid.Empty, EmailTemplateApiConstantsResponseMessage.email_template_see_try_catch);
            }
            return Ok(apiResponse);
        }

        [Authorize(Policy = PermissionConstants.EmailTemplateDelete)]
        [HttpDelete("delete-email-template")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteEmailTemplate(Guid templateId)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _emailTemplateService.Delete(templateId);
                    if (response.IsSuccess)
                    {
                        ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, EmailTemplateApiConstantsResponseMessage.email_template_delete_success_message);
                        return Ok(apiResponse);
                    }
                    else
                    {
                        ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                        return Ok(apiResponse);
                    }
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, EmailTemplateApiConstantsResponseMessage.email_template_deleted_failed_message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, EmailTemplateApiConstantsResponseMessage.email_template_see_try_catch);
            }
            return Ok(apiResponse);
        }
    }
}