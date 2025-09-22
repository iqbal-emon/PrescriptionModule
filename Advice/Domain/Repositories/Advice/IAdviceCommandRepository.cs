using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Advice.Domain.Repositories.Advice
{
    public interface IAdviceCommandRepository: IBaseCommonCommandMethodRepository<Entities.EntityClass.CommonAdvice>
    {
    }
}
