using DataAccess.DatabaseAccessLayer;
using Doctor.Application.Services;
using Doctor.Domain.Repositories.Doctor;
using Doctor.Insfracture.RepositoriesImplement.Doctor;
using Doctor.Domain.Repositories.DoctorChamber;
using Doctor.Insfracture.RepositoriesImplement.DoctorChamber;
using Doctor.Domain.Repositories.DoctorDegree;
using Doctor.Insfracture.RepositoriesImplement.DoctorDegree;
using Doctor.Domain.Repositories.DoctorExpertise;
using Doctor.Insfracture.RepositoriesImplement.DoctorExpertise;
using Doctor.Domain.Repositories.DoctorSchedule;
using Doctor.Insfracture.RepositoriesImplement.DoctorSchedule;
using Doctor.Domain.Repositories.DoctorSpecialization;
using Doctor.Insfracture.RepositoriesImplement.DoctorSpecialization;
using Doctor.Domain.Repositories.DoctorScheduleDaySession;
using Doctor.Insfracture.RepositoriesImplement.DoctorScheduleDaySession;
using Doctor.Domain.Repositories.DoctorScheduledDayOff;
using Doctor.Insfracture.RepositoriesImplement.DoctorScheduledDayOff;
using Doctor.Domain.Repositories.DoctorFeesSetup;
using Doctor.Insfracture.RepositoriesImplement.DoctorFeesSetup;
using Doctor.Domain.Repositories.MasterDoctor;
using Doctor.Insfracture.RepositoriesImplement.MasterDoctor;
using Doctor.Domain.Repositories.CampaignDoctor;
using Doctor.Insfracture.RepositoriesImplement.CampaignDoctor;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;
using Speciality.Application.Services;
using Speciality.Domain.Repositories.Speciality;
using Speciality.Insfracture.RepositoriesImplement.Speciality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            // Shared Services
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();

            // Doctor Services
            services.AddScoped<IDoctorCommandRepository, DoctorCommandRepository>();
            services.AddScoped<IDoctorQueryRepository, DoctorQueryRepository>();
            services.AddScoped<DoctorService>();

            // DoctorChamber Services
            services.AddScoped<IDoctorChamberQueryRepository, DoctorChamberQueryRepository>();
            services.AddScoped<IDoctorChamberCommandRepository, DoctorChamberCommandRepository>();
            services.AddScoped<DoctorChamberService>();

            // DoctorDegree Services
            services.AddScoped<IDoctorDegreeQueryRepository, DoctorDegreeQueryRepository>();
            services.AddScoped<IDoctorDegreeCommandRepository, DoctorDegreeCommandRepository>();
            services.AddScoped<DoctorDegreeService>();

            // DoctorExpertise Services
            services.AddScoped<IDoctorExpertiseQueryRepository, DoctorExpertiseQueryRepository>();
            services.AddScoped<IDoctorExpertiseCommandRepository, DoctorExpertiseCommandRepository>();
            services.AddScoped<DoctorExpertiseService>();

            // DoctorSchedule Services
            services.AddScoped<IDoctorScheduleQueryRepository, DoctorScheduleQueryRepository>();
            services.AddScoped<IDoctorScheduleCommandRepository, DoctorScheduleCommandRepository>();
            services.AddScoped<DoctorScheduleService>();

            // DoctorSpecialization Services
            services.AddScoped<IDoctorSpecializationQueryRepository, DoctorSpecializationQueryRepository>();
            services.AddScoped<IDoctorSpecializationCommandRepository, DoctorSpecializationCommandRepository>();
            services.AddScoped<DoctorSpecializationService>();

            // DoctorScheduleDaySession Services
            services.AddScoped<IDoctorScheduleDaySessionQueryRepository, DoctorScheduleDaySessionQueryRepository>();
            services.AddScoped<IDoctorScheduleDaySessionCommandRepository, DoctorScheduleDaySessionCommandRepository>();
            services.AddScoped<DoctorScheduleDaySessionService>();

            // DoctorScheduledDayOff Services
            services.AddScoped<IDoctorScheduledDayOffQueryRepository, DoctorScheduledDayOffQueryRepository>();
            services.AddScoped<IDoctorScheduledDayOffCommandRepository, DoctorScheduledDayOffCommandRepository>();
            services.AddScoped<DoctorScheduledDayOffService>();

            // DoctorFeesSetup Services
            services.AddScoped<IDoctorFeesSetupQueryRepository, DoctorFeesSetupQueryRepository>();
            services.AddScoped<IDoctorFeesSetupCommandRepository, DoctorFeesSetupCommandRepository>();
            services.AddScoped<DoctorFeesSetupService>();

            // MasterDoctor Services
            services.AddScoped<IMasterDoctorQueryRepository, MasterDoctorQueryRepository>();
            services.AddScoped<IMasterDoctorCommandRepository, MasterDoctorCommandRepository>();
            services.AddScoped<MasterDoctorService>();

            // CampaignDoctor Services
            services.AddScoped<ICampaignDoctorQueryRepository, CampaignDoctorQueryRepository>();
            services.AddScoped<ICampaignDoctorCommandRepository, CampaignDoctorCommandRepository>();
            services.AddScoped<CampaignDoctorService>();

            // Speciality Services
            services.AddScoped<ISpecialityQueryRepository, SpecialityQueryRepository>();
            services.AddScoped<ISpecialityCommandRepository, SpecialityCommandRepository>();
            services.AddScoped<Speciality.Application.Services.SpecialityService>();
        }
    }
}
