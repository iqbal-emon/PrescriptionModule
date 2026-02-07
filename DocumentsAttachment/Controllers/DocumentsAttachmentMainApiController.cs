using DocumentsAttachment.Application.Services;
using DocumentsAttachment.Dtos.RequestDto.DocumentsAttachmentDto;
using DocumentsAttachment.Dtos.ResponseDto.DocumentsAttachmentDto;
using DocumentsAttachment.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using Utility.ApiResponse;
using Utility.Permission;
using Utility.Response;

namespace DocumentsAttachment.Controllers
{
    [ApiController]
    [Route("api/2025-02/documents-attachment")]
    public class DocumentsAttachmentMainApiController : ControllerBase
    {
        private readonly DocumentsAttachmentService _attachmentService;
        private readonly MapperService _mapperService;

        public DocumentsAttachmentMainApiController(DocumentsAttachmentService attachmentService, MapperService mapperService)
        {
            _attachmentService = attachmentService;
            _mapperService = mapperService;
        }

        [HttpPost]
        [Authorize(Policy = PermissionConstants.DocumentsAttachmentCreate)]
        public async Task<ActionResult<ApiResponse<int>>> CreateDocumentAttachment([FromBody] DocumentsAttachmentInsertRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                var response = await _attachmentService.Insert(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Document attachment created successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionConstants.DocumentsAttachmentDelete)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAttachment(int id)
        {
            var apiResponse = new ApiResponse<bool>();
            try
            {
                var response = await _attachmentService.Delete(id);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Document attachment deleted successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, false, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, false, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.DocumentsAttachmentGetId)]
        public async Task<ActionResult<ApiResponse<DocumentsAttachmentApiResponseDto>>> GetAttachmentById(int id)
        {
            var apiResponse = new ApiResponse<DocumentsAttachmentApiResponseDto>();
            try
            {
                var attachment = await _attachmentService.GetById(id);
                if (attachment.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Document attachment not found");
                    return Ok(apiResponse);
                }

                var mappedAttachment = await _mapperService.MapSingle<Entities.EntityClass.DocumentsAttachment, DocumentsAttachmentApiResponseDto>(attachment.Result);
                apiResponse.Results = mappedAttachment;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Document attachment retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("attachment-info/{entityId}")]
        [Authorize(Policy = PermissionConstants.DocumentsAttachmentGetAll)]
        public async Task<ActionResult<ApiResponse<List<DocumentsAttachmentApiResponseDto>>>> GetAttachmentInfo(
            int entityId,
            [FromQuery] string entityType = "",
            [FromQuery] string attachmentType = "",
            [FromQuery] int? relatedEntityid = null)
        {
            var apiResponse = new ApiResponse<List<DocumentsAttachmentApiResponseDto>>();
            try
            {
                var attachments = await _attachmentService.GetByEntityIdAndType(entityId, entityType, attachmentType, relatedEntityid);
                if (attachments.Result == null || attachments.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DocumentsAttachmentApiResponseDto>(), "No attachments found");
                    return Ok(apiResponse);
                }

                var mappedAttachments = await _mapperService.MapList<Entities.EntityClass.DocumentsAttachment, DocumentsAttachmentApiResponseDto>(attachments.Result);
                apiResponse.Results = mappedAttachments;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Attachments retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DocumentsAttachmentApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet("document-info/{entityId}")]
        [Authorize(Policy = PermissionConstants.DocumentsAttachmentGetId)]
        public async Task<ActionResult<ApiResponse<DocumentsAttachmentApiResponseDto>>> GetDocumentInfo(
            int entityId,
            [FromQuery] string entityType = "",
            [FromQuery] string attachmentType = "")
        {
            var apiResponse = new ApiResponse<DocumentsAttachmentApiResponseDto>();
            try
            {
                var attachment = await _attachmentService.GetDocumentInfo(entityId, entityType, attachmentType);
                if (attachment.Result == null)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, null, "Document info not found");
                    return Ok(apiResponse);
                }

                var mappedAttachment = await _mapperService.MapSingle<Entities.EntityClass.DocumentsAttachment, DocumentsAttachmentApiResponseDto>(attachment.Result);
                apiResponse.Results = mappedAttachment;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Document info retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, null, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.DocumentsAttachmentGetAll)]
        public async Task<ActionResult<ApiResponse<List<DocumentsAttachmentApiResponseDto>>>> GetPaginatedAttachments(
            [FromQuery] string sorting = "",
            [FromQuery] int skipCount = 0,
            [FromQuery] int maxResultCount = 10)
        {
            var apiResponse = new ApiResponse<List<DocumentsAttachmentApiResponseDto>>();
            try
            {
                var attachments = await _attachmentService.GetPaginated(sorting, skipCount, maxResultCount);
                if (attachments.Result == null || attachments.Result.Count == 0)
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, new List<DocumentsAttachmentApiResponseDto>(), "No attachments found");
                    return Ok(apiResponse);
                }

                var mappedAttachments = await _mapperService.MapList<Entities.EntityClass.DocumentsAttachment, DocumentsAttachmentApiResponseDto>(attachments.Result);
                apiResponse.Results = mappedAttachments;
                ApiResponseHelper.SetSuccessResponse(apiResponse, apiResponse.Results, "Attachments retrieved successfully", StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, new List<DocumentsAttachmentApiResponseDto>(), $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PermissionConstants.DocumentsAttachmentUpdate)]
        public async Task<ActionResult<ApiResponse<int>>> UpdateAttachment(int id, [FromBody] DocumentsAttachmentUpdateRequestDto request)
        {
            var apiResponse = new ApiResponse<int>();
            try
            {
                request.DocumentsAttachmentID = id;
                var response = await _attachmentService.Update(request);
                if (response.IsSuccess)
                {
                    ApiResponseHelper.SetSuccessResponse(apiResponse, response.Result, "Document attachment updated successfully");
                }
                else
                {
                    ApiResponseHelper.SetFailedResponse(apiResponse, 0, response.Message);
                }
            }
            catch (Exception ex)
            {
                ApiResponseHelper.SetFailedResponse(apiResponse, 0, $"Error: {ex.Message}");
            }
            return Ok(apiResponse);
        }
    }
}

