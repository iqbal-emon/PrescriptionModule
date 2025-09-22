using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace FollowUp.Domain.Repositories.FollowUp
{
    public interface IFollowUpCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.FollowUp>
    {
    }
}
