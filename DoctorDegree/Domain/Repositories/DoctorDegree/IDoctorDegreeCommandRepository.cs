using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace DoctorDegree.Domain.Repositories.DoctorDegree
{
    public interface IDoctorDegreeCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.DoctorDegree>
    {
        
    }
}
