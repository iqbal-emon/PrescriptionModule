using AuthenticationSystem.Application.Models.MiddlewareModels;
using AuthenticationSystem.Domain.Repositories.Permission;
using Entities.EntityClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationSystem.Application.Services
{
    public class AuthPermissionService : IPermissionService
    {
        private readonly IPermissionQueryRepository _permissionQueryRepository;

        public AuthPermissionService(IPermissionQueryRepository permissionQueryRepository)
        {
            _permissionQueryRepository = permissionQueryRepository;
        }

        public List<PermissionModel> GetPermissions()
        {
            try
            {
                var permissionsResponse = Task.Run(async () => await _permissionQueryRepository.GetAll()).Result;
                if (permissionsResponse?.Result == null)
                {
                    return new List<PermissionModel>();
                }

                var permissionModels = permissionsResponse.Result.Select(p => new PermissionModel
                {
                    PolicyName = p.DisplayName,
                    PermissionKey = p.PermissionValue
                }).ToList();

                return permissionModels;
            }
            catch (Exception)
            {
                return new List<PermissionModel>();
            }
        }
    }
}

