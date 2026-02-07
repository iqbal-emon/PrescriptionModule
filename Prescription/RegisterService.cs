using ApiCallService.BaseApiCallService;
using DataAccess.DatabaseAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using PluginDIService.PluginDependencyRepository;
using Prescription.Application.services;
using Prescription.Application.Services;
using Prescription.Domain.Repositories.Prescription;
using Prescription.Domain.Repositories.PrescriptionAdvice;
using Prescription.Domain.Repositories.PrescriptionDiagonosis;
using Prescription.Domain.Repositories.PrescriptionExamination;
using Prescription.Domain.Repositories.PrescriptionFollowUp;
using Prescription.Domain.Repositories.PrescriptionInvestigation;
using Prescription.Domain.Repositories.PrescriptionItem;
using Prescription.Domain.Repositories.PrescriptionPatientHistory;
using Prescription.Domain.Repositories.PrescriptionSymtom;
using Prescription.Domain.Repositories.PrescriptionTemplate;
using Prescription.Domain.Repositories.ScannedPrescription;
using Prescription.Insfracture.RepositoriesImplement.PrescriptioinFollowUp;
using Prescription.Insfracture.RepositoriesImplement.Prescription;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionAdvice;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionDiagonosis;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionExamination;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionInvestigation;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionItem;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionPatientHistory;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionSymptom;
using Prescription.Insfracture.RepositoriesImplement.PrescriptionTemplate;
using Prescription.Insfracture.RepositoriesImplement.ScannedPrescription;
using PrescriptionExamination.Application.Services;
using SharedService.CommonService;
using SharedService.JWTTokenService;
using SharedService.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription
{
    public class RegisterService : IPlugin
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SharedCommonService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPrescriptionQueryRepository, PrescriptionQueryRepository>();
            services.AddScoped<IPrescriptionCommandRepository, PrescriptionCommandRepository>();
            services.AddScoped<ISqlDataAccessLayer, SqlDataAccessLayer>();
            services.AddScoped<MapperService>();
            services.AddScoped<PrescriptionService>();


            services.AddScoped<IPrescriptionAdviceQueryRepository, PrescriptionAdviceQueryRepository>();
            services.AddScoped<IPrescriptionAdviceCommandRepository, PrescriptionAdviceCommandRepository>();
            services.AddScoped<PrescriptionAdviceService>();


            services.AddScoped<IPrescriptionDiagonsisQueryRepository, PrescriptionDiagonsisQueryRepository>();
            services.AddScoped<IPrescriptionDiagonsisCommandRepository, PrescriptionDiagonsisCommandRepository>();
            services.AddScoped<PrescriptionDiagonsisService>();


            // Register services specific to PrescriptionFollowUp
            services.AddScoped<IPrescriptionFollowUpQueryRepository, PrescriptionFollowUpQueryRepository>();
            services.AddScoped<IPrescriptionFollowUpCommandRepository, PrescriptionFollowUpCommandRepository>();
            services.AddScoped<PrescriptionFollowUpService>();

            // Register services specific to PrescriptionInvestigation
            services.AddScoped<IPrescriptionInvestigationQueryRepository, PrescriptionInvestigationQueryRepository>();
            services.AddScoped<IPrescriptionInvestigationCommandRepository, PrescriptionInvestigationCommandRepository>();
            services.AddScoped<PrescriptionInvestigationService>();


            // Register services specific to PrescriptionItem
            services.AddScoped<IPrescriptionItemQueryRepository, PrescriptionItemQueryRepository>();
            services.AddScoped<IPrescriptionItemCommandRepository, PrescriptionItemCommandRepository>();
            services.AddScoped<PrescriptionItemService>();

            // Register services specific to DoctorPrescription
            services.AddScoped<IPrescriptionPatientHistoryQueryRepository, PrescriptionPatientHistoryQueryRepository>();
            services.AddScoped<IPrescriptionPatientHistoryCommandRepository, PrescriptionPatientHistoryCommandRepository>();
            services.AddScoped<PrescriptionPatientHistoryService>();

            // Register services specific to PrescriptionSymtom
            services.AddScoped<IPrescriptionSymtomQueryRepository, PrescriptionSymptomQueryRepository>();
            services.AddScoped<IPrescriptionSymtomCommandRepository, PrescriptionSymptomCommandRepository>();
            services.AddScoped<PrescriptionSymptomService>();

            // Register services specific to PrescriptionExminatioin
            services.AddScoped<IPrescriptionExminationQueryRepository, PrescriptionExaminationQueryRepository>();
            services.AddScoped<IPrescriptionExminationCommandRepository, PrescriptionExaminationCommandRepository>();
            services.AddScoped<PrescriptionExaminationService>();




            services.AddScoped<IScannedPrescrptionCommandRepository, ScannedPrescriptionCommandRepository>();
            services.AddScoped<IScannedPrescrptionQueryRepository, ScannedPrescriptionQueryRepository>();
            services.AddScoped<ScannedPrescriptionService>();
            // Register services specific to DoctorPrescription
            services.AddScoped<IBaseRestClientApiService, BaseRestClientApiService>();

            // Register services specific to DoctorPrescription
            services.AddScoped<PrescriptionPatientService>();
            services.AddScoped<PrescriptionPdfCreatorService>();
            services.AddScoped<PrescriptionEmailTemplateService>();


            services.AddScoped<IPrescriptionTemplateQueryRepository, PrescriptionTemplateQueryRepository>();
            services.AddScoped<IPrescriptionTemplateCommandRepository, PrescriptionTemplateCommandRepository>();

            services.AddScoped<PrescriptionTemplateService>();

           




        }
    }
}
