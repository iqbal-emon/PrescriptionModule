using AuthenticationSystem.Application.MiddlewareService;
using System.Reflection;
using Utility.Permission;

namespace AuthenticationSystem
{
    public static class AuthorizationPolicySetup
    {
        public static void AddCustomAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var permissionFields = typeof(PermissionConstants)
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where(f => f.FieldType == typeof(string));

                foreach (var field in permissionFields)
                {
                    string permissionValue = field.GetValue(null) as string;
                    if (!string.IsNullOrEmpty(permissionValue))
                    {
                        options.AddPolicy(permissionValue, policy =>
                            policy.Requirements.Add(new PermissionRequirement(permissionValue)));
                    }
                }
            });


            //public static void AddCustomAuthorizationPolicies(this IServiceCollection services)
            //{
            //    services.AddAuthorization(options =>
            //    {
            //        options.AddPolicy("MedicationCreate", policy =>
            //            policy.Requirements.Add(new PermissionRequirement(PermissionConstants.MedicationCreate)));

            //        options.AddPolicy("MedicationUpdate", policy =>
            //            policy.Requirements.Add(new PermissionRequirement(PermissionConstants.MedicationUpdate)));

            //        options.AddPolicy("MedicationDelete", policy =>
            //            policy.Requirements.Add(new PermissionRequirement(PermissionConstants.MedicationDelete)));

            //        options.AddPolicy("MedicationGetAll", policy =>
            //            policy.Requirements.Add(new PermissionRequirement(PermissionConstants.MedicationGetAll)));

            //        options.AddPolicy("MedicationGetId", policy =>
            //            policy.Requirements.Add(new PermissionRequirement(PermissionConstants.MedicationGetId)));

            //    });

            //using var scope = services.BuildServiceProvider().CreateScope();
            //var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            //var permissions = permissionService.GetPermissions(); // Fetch permissions from DB

            //services.AddAuthorization(options =>
            //{
            //    foreach (var permission in permissions)
            //    {
            //        options.AddPolicy(permission.PolicyName, policy =>
            //            policy.Requirements.Add(new PermissionRequirement(permission.PermissionKey)));
            //    }
            //});
        }
    }
}
