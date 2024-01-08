using FinancialDocumentRetrieval.BL.AutoMapperProfiles;
using FinancialDocumentRetrieval.BL.Implementation;
using FinancialDocumentRetrieval.BL.Implementation.Factory;
using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.BL.Interface.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialDocumentRetrieval.DAL
{
    public static class BLDependencyInjection
    {
        public static IServiceCollection AddBL(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            services.AddServices();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAnonymizationService, AnonymizationService>();
            services.AddScoped<IFinancialDocumentService, FinancialDocumentService>();
            services.AddScoped<IValidationFactory, ValidationFactory>();
            services.AddScoped<IAuthManager, AuthManager>();
        }
    }
}
