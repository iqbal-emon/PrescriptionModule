using AuthenticationSystem.Application.Models.MiddlewareModels;

namespace AuthenticationSystem.Application.Services
{
    public interface IPermissionService
    {
        List<PermissionModel> GetPermissions();
    }
}
