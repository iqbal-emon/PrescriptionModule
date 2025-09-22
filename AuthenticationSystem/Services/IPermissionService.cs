using AuthenticationSystem.Models.MiddlewareModels;

namespace AuthenticationSystem.Services
{
    public interface IPermissionService
    {
        List<PermissionModel> GetPermissions();
    }
}
