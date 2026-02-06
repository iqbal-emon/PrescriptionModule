using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorDegree
{
    public interface IDoctorDegreeQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DoctorDegree>
    {
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorDegree>>> GetByDoctorId(int doctorId);
    }
}

