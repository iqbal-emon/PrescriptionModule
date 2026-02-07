using DataAccess.DatabaseAccessLayer;
using DocumentsAttachment.Application.Services;
using DocumentsAttachment.Domain.Repositories.DocumentsAttachment;
using DocumentsAttachment.Insfracture.RepositoriesImplement.DocumentsAttachment;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;

namespace DocumentsAttachment
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDocumentsAttachmentQueryRepository, DocumentsAttachmentQueryRepository>();
            services.AddScoped<IDocumentsAttachmentCommandRepository, DocumentsAttachmentCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<DocumentsAttachmentService>();

        }

    }
}

