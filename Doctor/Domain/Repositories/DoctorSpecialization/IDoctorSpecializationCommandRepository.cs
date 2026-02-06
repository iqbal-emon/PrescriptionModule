using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorSpecialization
{
    public interface IDoctorSpecializationCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.DoctorSpecialization>
    {
    }
}

