using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BaseInterface;
using Utility.Response;

namespace Specialization.Domain.Repositories.Specialization
{
    public interface ISpecializationQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.Specialization>
    {
        Task<Response<List<Entities.EntityClass.Specialization>>> GetBySpecialityId(int specialityId);
        Task<Response<List<Entities.EntityClass.Specialization>>> GetFiltered();
    }
}

