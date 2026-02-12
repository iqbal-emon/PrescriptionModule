using Entities.EntityClass;
using Utility.BaseInterface;
using Utility.Response;

namespace AuthenticationSystem.Domain.Repositories.User
{
    public interface IUserQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.User>
    {
        Task<Response<Entities.EntityClass.User>> GetByEmail(string email);
        Task<Response<Entities.EntityClass.User>> GetByUserName(string userName);
        Task<Response<Entities.EntityClass.User>> GetByPhoneNo(string phoneNo);
        Task<Response<List<Entities.EntityClass.User>>> GetByTenantId(int tenantId);
        Task<Response<List<Entities.EntityClass.User>>> GetByUserType(string userType);
        Task<Response<List<Entities.EntityClass.User>>> GetByRoleId(int roleId);
    }
}

