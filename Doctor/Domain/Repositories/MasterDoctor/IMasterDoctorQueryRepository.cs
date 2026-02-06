using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.MasterDoctor
{
    public interface IMasterDoctorQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.DoctorEntity.MasterDoctor>
    {
        Task<Response<List<Entities.EntityClass.DoctorEntity.MasterDoctor>>> GetByDoctorId(int doctorId);
    }
}

