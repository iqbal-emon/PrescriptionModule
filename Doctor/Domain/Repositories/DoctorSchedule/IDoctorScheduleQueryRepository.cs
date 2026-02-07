using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorSchedule
{
    public interface IDoctorScheduleQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DoctorSchedule>
    {
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetByDoctorId(int doctorId);
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSchedule>>> GetByDoctorIdAndChamberId(int doctorId, int chamberId);
    }
}

