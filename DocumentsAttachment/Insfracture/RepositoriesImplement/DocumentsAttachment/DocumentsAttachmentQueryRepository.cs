using DataAccess.DatabaseAccessLayer;
using DocumentsAttachment.Domain.Repositories.DocumentsAttachment;
using DocumentsAttachment.Utility;
using Entities.EntityClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;
using Utility.Response;
using Utility.SqlErrorMessgae;

namespace DocumentsAttachment.Insfracture.RepositoriesImplement.DocumentsAttachment
{
    public class DocumentsAttachmentQueryRepository : IDocumentsAttachmentQueryRepository
    {
        private readonly ISqlDataAccessLayer _dataAccess;
        public DocumentsAttachmentQueryRepository(ISqlDataAccessLayer dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Get all Documents Attachments
        public Task<Response<List<TEntity>>> GetAll<TEntity>() where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetAll()
        {
            var response = new Response<List<Entities.EntityClass.DocumentsAttachment>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DocumentsAttachment, dynamic>("DocumentsAttachment_GetAll", new
                {
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DocumentsAttachmentResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Documents Attachment by ID
        public async Task<Response<Entities.EntityClass.DocumentsAttachment>> GetById(int id)
        {
            var response = new Response<Entities.EntityClass.DocumentsAttachment>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DocumentsAttachment, dynamic>("DocumentsAttachment_GetById", new
                {
                    DocumentsAttachmentID = id
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DocumentsAttachmentResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get by Entity ID and Type
        public async Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetByEntityIdAndType(int entityId, string entityType, string attachmentType, int? relatedEntityid = null)
        {
            var response = new Response<List<Entities.EntityClass.DocumentsAttachment>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DocumentsAttachment, dynamic>("DocumentsAttachment_GetByEntityIdAndType", new
                {
                    EntityId = entityId,
                    EntityType = entityType,
                    AttachmentType = attachmentType,
                    RelatedEntityid = relatedEntityid
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DocumentsAttachmentResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Document Info
        public async Task<Response<Entities.EntityClass.DocumentsAttachment>> GetDocumentInfo(int entityId, string entityType, string attachmentType)
        {
            var response = new Response<Entities.EntityClass.DocumentsAttachment>();
            try
            {
                var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.DocumentsAttachment, dynamic>("DocumentsAttachment_GetDocumentInfo", new
                {
                    EntityId = entityId,
                    EntityType = entityType,
                    AttachmentType = attachmentType
                });
                response.Result = result;
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DocumentsAttachmentResponseMessage.common_get_by_id_success, StatusResponseMessage.success, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                response.Message = StandardDataAccessMessages.GetSqlErrorMessage(ex);
                ResponseHelper.SetFailedResponse(response, null, response.Message, StatusResponseMessage.failed, StatusCodes.Status400BadRequest);
            }
            return response;
        }

        // Get Paginated
        public async Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetPaginated(string sorting = "", int skipCount = 0, int maxResultCount = 10)
        {
            var response = new Response<List<Entities.EntityClass.DocumentsAttachment>>();
            try
            {
                var result = await _dataAccess.LoadDataUsingProcedure<Entities.EntityClass.DocumentsAttachment, dynamic>("DocumentsAttachment_GetPaginated", new
                {
                    Sorting = sorting,
                    SkipCount = skipCount,
                    MaxResultCount = maxResultCount
                });
                response.Result = result.ToList();
                response.IsSuccess = true;
                ResponseHelper.SetSuccessResponse(response, result, DocumentsAttachmentResponseMessage.common_get_all_success, StatusResponseMessage.success, StatusCodes.Status200OK);
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

