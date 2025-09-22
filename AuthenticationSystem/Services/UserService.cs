using AuthenticationSystem.Models.MiddlewareModels;
using Utility.Permission;

namespace AuthenticationSystem.Services
{
    public class UserService : IUserService
    {
        //private readonly MyDbContext _context;

        public UserService(/*MyDbContext context*/)
        {
            //_context = context;
        }

        public async Task<List<string>> GetUserPermissions(int userId)
        {
            //return _context.UserPermissions
            //    .Where(up => up.UserId == userId)
            //    .Select(up => up.PermissionKey)
            //    .ToList();
            return new List<string>
            {
                PermissionConstants.MedicationCreate,
                PermissionConstants.MedicationUpdate,
                PermissionConstants.MedicationDelete
            };
        }
    }
}
