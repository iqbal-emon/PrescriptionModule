using AuthenticationSystem.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Utility.Permission;

namespace AuthenticationSystem.Application.MiddlewareService
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // Assuming you have a method to extract the user ID from the token
                var userId = GetUserIdFromToken(token);

                if (userId != null)
                {
                    // Retrieve user details (permissions)
                    //var userPermissions = await userService.GetUserPermissions(Convert.ToInt32(userId));

                    var userPermissions = typeof(PermissionConstants)
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where(f => f.FieldType == typeof(string))
                    .Select(f => f.GetValue(null)?.ToString())
                    .Where(value => !string.IsNullOrEmpty(value))
                    .ToList();

                    context.Items["UserPermissions"] = userPermissions;

                    // Attach user permissions to the context for use in authorization
                    //context.Items["UserPermissions"] = userPermissions;
                }
            }

            var permissions = typeof(PermissionConstants)
             .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
             .Where(f => f.FieldType == typeof(string))
             .Select(f => f.GetValue(null)?.ToString())
             .Where(value => !string.IsNullOrEmpty(value))
             .ToList();

            context.Items["UserPermissions"] = permissions;
            await _next(context);
        }
        private string GetUserIdFromToken(string token)
        {
            // Logic to decode the token and extract user ID (e.g., using JWT)
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            return jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        }
    }
}
