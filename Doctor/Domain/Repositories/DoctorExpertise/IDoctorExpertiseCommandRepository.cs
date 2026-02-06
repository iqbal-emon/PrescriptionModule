using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorExpertise
{
    public interface IDoctorExpertiseCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.DoctorExpertise>
    {
    }
}

