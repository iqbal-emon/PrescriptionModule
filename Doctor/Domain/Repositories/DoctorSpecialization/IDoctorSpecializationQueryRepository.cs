using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorSpecialization
{
    public interface IDoctorSpecializationQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DoctorSpecialization>
    {
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>> GetByDoctorId(int doctorId);
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>> GetBySpecialityId(int specialityId);
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorSpecialization>>> GetByDoctorIdAndSpecialityId(int doctorId, int specialityId);
    }
}

