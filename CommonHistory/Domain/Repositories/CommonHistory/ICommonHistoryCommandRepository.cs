using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace CommonHistory.Domain.Repositories.CommonHistory
{
    public interface ICommonHistoryCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.CommonHistory>
    {
    }
}
