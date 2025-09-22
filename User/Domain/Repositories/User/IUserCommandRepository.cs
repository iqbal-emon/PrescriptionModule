using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace User.Domain.Repositories.User
{
    public interface IUserCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.User>
    {
    }
}
