using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationSystem.MiddlewareService
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // Check if the Resource is of type HttpContext
            if (context.Resource is HttpContext httpContext)
            {
                var userPermissions = httpContext.Items["UserPermissions"] as List<string>;

                // If the user has the required permissions, authorize the request
                if (userPermissions != null && userPermissions.Contains(requirement.Permission))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
