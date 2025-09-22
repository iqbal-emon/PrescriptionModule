using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Doctor.Domain.Repositories.Doctor
{
    public interface IDoctorQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.Doctor>
    {
        Task<Response <Entities.EntityClass.Doctor>> GetByReferenceId(int id);
    }
}
