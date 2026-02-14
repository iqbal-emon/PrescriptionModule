using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorScheduleDaySession
{
    public interface IDoctorScheduleDaySessionQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>
    {
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorScheduleDaySession>>> GetByDoctorScheduleId(int doctorScheduleId);
    }
}

