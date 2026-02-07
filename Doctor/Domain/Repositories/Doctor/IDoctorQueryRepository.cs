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
        Task<Response<Entities.EntityClass.Doctor>> GetByUserName(string userName);
        Task<Response<Entities.EntityClass.Doctor>> GetByEmail(string email);
        Task<Response<List<Entities.EntityClass.Doctor>>> GetByOnlineStatus(bool isOnline);
        Task<Response<List<Entities.EntityClass.Doctor>>> GetByActiveStatus(bool isActive);
        Task<Response<List<Entities.EntityClass.Doctor>>> GetByCreatorId(int creatorId);
    }
}
