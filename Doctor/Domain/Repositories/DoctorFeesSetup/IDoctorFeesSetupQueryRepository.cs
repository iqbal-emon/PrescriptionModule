using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.DoctorFeesSetup
{
    public interface IDoctorFeesSetupQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>
    {
        Task<Response<List<Entities.EntityClass.DoctorEntity.DoctorFeesSetup>>> GetByDoctorId(int doctorId);
    }
}

