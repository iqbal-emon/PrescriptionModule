using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace EmailTemplate.Domain.Repositories.EmailTemplate
{
    public interface IEmailTemplateCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.EmailTemplate>
    {

        Task<Response<bool>> Delete(Guid id);
        Task<Response<Guid>> Insert(Entities.EntityClass.EmailTemplate entity);
        Task<Response<Guid>> Update(Entities.EntityClass.EmailTemplate entity);
    }
}
