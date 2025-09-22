using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace DoctorSchedule.Domain.Repositories.DoctorSchedule
{
    public interface IDoctorScheduleCommandRepository : IBaseCommonCommandMethodRepository<Entities.EntityClass.DoctorEntity.DoctorSchedule>
    {
        
    }
}
