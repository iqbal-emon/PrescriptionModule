using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;


namespace User.Domain.Repositories.User
{
    public interface IUserQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.User>
    {
        
             Task<Response <Entities.EntityClass.User>> GetByRoleAndReferenceId(int referenceId,string role,int tenantId);
    }
}
