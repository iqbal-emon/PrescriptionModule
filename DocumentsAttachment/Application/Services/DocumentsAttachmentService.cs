using DocumentsAttachment.Domain.Repositories.DocumentsAttachment;
using DocumentsAttachment.Dtos.RequestDto.DocumentsAttachmentDto;
using Microsoft.AspNetCore.Http;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;

namespace DocumentsAttachment.Application.Services
{
    public class DocumentsAttachmentService
    {
        private readonly IDocumentsAttachmentQueryRepository _attachmentQueryRepository;
        private readonly IDocumentsAttachmentCommandRepository _attachmentCommandRepository;
        private readonly MapperService _mapperService;

        public DocumentsAttachmentService(
            IDocumentsAttachmentQueryRepository attachmentQueryRepository,
            IDocumentsAttachmentCommandRepository attachmentCommandRepository,
            MapperService mapperService)
        {
            _attachmentQueryRepository = attachmentQueryRepository;
            _attachmentCommandRepository = attachmentCommandRepository;
            _mapperService = mapperService;
        }

        public async Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DocumentsAttachment>>();

            try
            {
                var attachments = await _attachmentQueryRepository.GetAll();

                if (attachments == null || !attachments.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, attachments?.Result, attachments?.Message ?? "Failed to retrieve documents attachments", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, attachments.Result, attachments.Message, StatusResponseMessage.success, attachments.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Documents Attachment.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DocumentsAttachment>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DocumentsAttachment>();

            try
            {
                var attachment = await _attachmentQueryRepository.GetById(id);

                if (attachment == null || !attachment.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, attachment?.Result, attachment?.Message ?? "Document attachment not found", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, attachment.Result, attachment.Message, StatusResponseMessage.success, attachment.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Documents Attachment.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetByEntityIdAndType(int entityId, string entityType, string attachmentType, int? relatedEntityid = null)
        {
            var response = new Response<List<Entities.EntityClass.DocumentsAttachment>>();

            try
            {
                var attachments = await _attachmentQueryRepository.GetByEntityIdAndType(entityId, entityType, attachmentType, relatedEntityid);

                if (attachments == null || !attachments.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, attachments?.Result, attachments?.Message ?? "Failed to retrieve documents attachments", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, attachments.Result, attachments.Message, StatusResponseMessage.success, attachments.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Documents Attachments.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<Entities.EntityClass.DocumentsAttachment>> GetDocumentInfo(int entityId, string entityType, string attachmentType)
        {
            var response = new Response<Entities.EntityClass.DocumentsAttachment>();

            try
            {
                var attachment = await _attachmentQueryRepository.GetDocumentInfo(entityId, entityType, attachmentType);

                if (attachment == null || !attachment.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, attachment?.Result, attachment?.Message ?? "Document info not found", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, attachment.Result, attachment.Message, StatusResponseMessage.success, attachment.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Document Info.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetPaginated(string sorting = "", int skipCount = 0, int maxResultCount = 10)
        {
            var response = new Response<List<Entities.EntityClass.DocumentsAttachment>>();

            try
            {
                var attachments = await _attachmentQueryRepository.GetPaginated(sorting, skipCount, maxResultCount);

                if (attachments == null || !attachments.IsSuccess)
                {
                    ResponseHelper.SetFailedResponse(response, attachments?.Result, attachments?.Message ?? "Failed to retrieve paginated documents attachments", StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
                }
                else
                {
                    ResponseHelper.SetSuccessResponse(response, attachments.Result, attachments.Message, StatusResponseMessage.success, attachments.StatusCode);
                }
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while retrieving the Documents Attachments.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred.";
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Insert(DocumentsAttachmentInsertRequestDto attachment)
        {
            var response = new Response<int>();

            try
            {
                var attachmentEntity = await _mapperService.MapSingle<DocumentsAttachmentInsertRequestDto, Entities.EntityClass.DocumentsAttachment>(attachment);
                attachmentEntity.CreatedAt = DateTime.Now;
                var insertResponse = await _attachmentCommandRepository.Insert(attachmentEntity);

                response = insertResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while inserting the Documents Attachment.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while inserting the Documents Attachment.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<int>> Update(DocumentsAttachmentUpdateRequestDto attachment)
        {
            var response = new Response<int>();

            try
            {
                var attachmentEntity = await _mapperService.MapSingle<DocumentsAttachmentUpdateRequestDto, Entities.EntityClass.DocumentsAttachment>(attachment);
                attachmentEntity.UpdatedAt = DateTime.Now;
                attachmentEntity.CreatedAt = DateTime.Now;
                var updatedResponse = await _attachmentCommandRepository.Update(attachmentEntity);

                response = updatedResponse;
            }
            catch (SqlException sqlEx)
            {
                response.Message = "A database error occurred while updating the Documents Attachment.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                response.Message = "An unexpected error occurred while updating the Documents Attachment.";
                ResponseHelper.SetFailedResponse(response, response.Result, response.Message, StatusResponseMessage.success, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public Task<Response<bool>> Delete(int id)
        {
            return _attachmentCommandRepository.Delete(id);
        }
    }
}

