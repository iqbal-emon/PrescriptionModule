using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace DocumentsAttachment.Domain.Repositories.DocumentsAttachment
{
    public interface IDocumentsAttachmentCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DocumentsAttachment>
    {
        
    }
}

