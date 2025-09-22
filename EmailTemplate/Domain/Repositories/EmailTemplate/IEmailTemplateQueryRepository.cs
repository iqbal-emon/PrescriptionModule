using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;
using static Dapper.SqlMapper;

namespace EmailTemplate.Domain.Repositories.EmailTemplate
{
    public interface IEmailTemplateQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.EmailTemplate>
    {
        Task<Response<Entities.EntityClass.EmailTemplate>> GetById(Guid id);

    }
}
