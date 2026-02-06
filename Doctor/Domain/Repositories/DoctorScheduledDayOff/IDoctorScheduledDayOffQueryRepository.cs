using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorScheduledDayOff
{
    public interface IDoctorScheduledDayOffQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DoctorScheduledDayOff>
    {
    }
}

