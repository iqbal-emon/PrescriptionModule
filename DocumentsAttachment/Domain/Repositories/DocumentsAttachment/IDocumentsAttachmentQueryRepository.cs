using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace DocumentsAttachment.Domain.Repositories.DocumentsAttachment
{
    public interface IDocumentsAttachmentQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DocumentsAttachment>
    {
        Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetByEntityIdAndType(int entityId, string entityType, string attachmentType, int? relatedEntityid = null);
        Task<Response<Entities.EntityClass.DocumentsAttachment>> GetDocumentInfo(int entityId, string entityType, string attachmentType);
        Task<Response<List<Entities.EntityClass.DocumentsAttachment>>> GetPaginated(string sorting = "", int skipCount = 0, int maxResultCount = 10);
    }
}

