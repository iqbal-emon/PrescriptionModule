using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Degree.Domain.Repositories.Degree
{
    public interface IDegreeCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.Degree>
    {
        
    }
}
