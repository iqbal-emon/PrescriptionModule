using AuthenticationSystem.Models.MiddlewareModels;
using Utility.Permission;

namespace AuthenticationSystem.Services
{
    public class PermissionService : IPermissionService
    {
       // private readonly MyDbContext _context; // Your database context

        public PermissionService(/*MyDbContext context*/)
        {
            //_context = context;
        }

        public List<PermissionModel> GetPermissions()
        {
            //return _context.Permissions
            //    .Select(p => new PermissionModel { PolicyName = p.PolicyName, PermissionKey = p.PermissionKey })
            //    .ToList();
            return new List<PermissionModel>
            {
                new PermissionModel { PolicyName = PermissionConstants.MedicationCreate, PermissionKey = PermissionConstants.MedicationCreate },
                new PermissionModel { PolicyName = PermissionConstants.MedicationUpdate, PermissionKey = PermissionConstants.MedicationUpdate },
                new PermissionModel { PolicyName = PermissionConstants.MedicationDelete, PermissionKey = PermissionConstants.MedicationDelete }
            };
        }
    }
}
