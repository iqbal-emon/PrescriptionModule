using Entities.EntityClass;
using Utility.BaseInterface;
using Utility.Response;

namespace AuthenticationSystem.Domain.Repositories.RolePermission
{
    public interface IRolePermissionQueryRepository : IBaseCommonQueryMethodRepository<Entities.EntityClass.RolePermission>
    {
        Task<Response<List<Entities.EntityClass.RolePermission>>> GetByRoleId(int roleId);
        Task<Response<List<Entities.EntityClass.RolePermission>>> GetByPermissionId(int permissionId);
    }
}

