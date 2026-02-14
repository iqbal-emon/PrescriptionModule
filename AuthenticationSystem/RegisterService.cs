using AuthenticationSystem.Application.Services;
using AuthenticationSystem.Domain.Repositories.Company;
using AuthenticationSystem.Domain.Repositories.CompanyBranch;
using AuthenticationSystem.Domain.Repositories.Permission;
using AuthenticationSystem.Domain.Repositories.Role;
using AuthenticationSystem.Domain.Repositories.RolePermission;
using AuthenticationSystem.Domain.Repositories.User;
using AuthenticationSystem.Insfrastructure.RepositoriesImplement.Company;
using AuthenticationSystem.Insfrastructure.RepositoriesImplement.CompanyBranch;
using AuthenticationSystem.Insfrastructure.RepositoriesImplement.Permission;
using AuthenticationSystem.Insfrastructure.RepositoriesImplement.Role;
using AuthenticationSystem.Insfrastructure.RepositoriesImplement.RolePermission;
using AuthenticationSystem.Insfrastructure.RepositoriesImplement.User;
using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace AuthenticationSystem
{
    public static class RegisterService
    {
        public static void AddAuthServices(this IServiceCollection services)
        {
            // Shared Services
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();

            // Company
            services.AddScoped<ICompanyQueryRepository, CompanyQueryRepository>();
            services.AddScoped<ICompanyCommandRepository, CompanyCommandRepository>();
            services.AddScoped<CompanyService>();

            // CompanyBranch
            services.AddScoped<ICompanyBranchQueryRepository, CompanyBranchQueryRepository>();
            services.AddScoped<ICompanyBranchCommandRepository, CompanyBranchCommandRepository>();
            services.AddScoped<CompanyBranchService>();

            // Permission
            services.AddScoped<IPermissionQueryRepository, PermissionQueryRepository>();
            services.AddScoped<IPermissionCommandRepository, PermissionCommandRepository>();
            services.AddScoped<PermissionService>();

            // Role
            services.AddScoped<IRoleQueryRepository, RoleQueryRepository>();
            services.AddScoped<IRoleCommandRepository, RoleCommandRepository>();
            services.AddScoped<RoleService>();

            // RolePermission
            services.AddScoped<IRolePermissionQueryRepository, RolePermissionQueryRepository>();
            services.AddScoped<IRolePermissionCommandRepository, RolePermissionCommandRepository>();
            services.AddScoped<RolePermissionService>();

            // User
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<UserService>();

            // Auth Services (for middleware)
            services.AddScoped<IUserService, AuthUserService>();
            services.AddScoped<AuthUserService>(); // Direct registration for controllers
            services.AddScoped<IPermissionService, AuthPermissionService>();

            // Firebase Service
            services.AddSingleton<FirebaseAuthService>();
        }
    }
}

