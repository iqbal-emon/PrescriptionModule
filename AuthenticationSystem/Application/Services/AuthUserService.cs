using AuthenticationSystem.Domain.Repositories.User;
using AuthenticationSystem.Domain.Repositories.RolePermission;
using AuthenticationSystem.Domain.Repositories.Permission;
using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationSystem.Application.Services
{
    public class AuthUserService : IUserService
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IRolePermissionQueryRepository _rolePermissionQueryRepository;
        private readonly IPermissionQueryRepository _permissionQueryRepository;

        public AuthUserService(
            IUserQueryRepository userQueryRepository,
            IRolePermissionQueryRepository rolePermissionQueryRepository,
            IPermissionQueryRepository permissionQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            _rolePermissionQueryRepository = rolePermissionQueryRepository;
            _permissionQueryRepository = permissionQueryRepository;
        }

        public async Task<List<string>> GetUserPermissions(int userId)
        {
            try
            {
                // Get user by ID
                var userResponse = await _userQueryRepository.GetById(userId);
                if (userResponse?.Result == null)
                {
                    return new List<string>();
                }

                // Get role permissions for the user's role
                var rolePermissionsResponse = await _rolePermissionQueryRepository.GetByRoleId(userResponse.Result.RoleId);
                if (rolePermissionsResponse?.Result == null || !rolePermissionsResponse.Result.Any())
                {
                    return new List<string>();
                }

                // Get all permissions
                var permissionsResponse = await _permissionQueryRepository.GetAll();
                if (permissionsResponse?.Result == null)
                {
                    return new List<string>();
                }

                // Get permission IDs from role permissions
                var permissionIds = rolePermissionsResponse.Result.Select(rp => rp.PermissionId).ToList();

                // Filter permissions and return permission values
                var userPermissions = permissionsResponse.Result
                    .Where(p => permissionIds.Contains(p.Id))
                    .Select(p => p.PermissionValue)
                    .ToList();

                return userPermissions;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}

