using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;

namespace Examinations.Domain.Repositories.Examinations
{
    public interface IExaminationsQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.Examination>
    {
    }
}
