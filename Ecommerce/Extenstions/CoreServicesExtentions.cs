using Domain.Contracts;
using Persistance.Data.DataSeeding;
using Persistance.Repositories;
using Services;
using Services.Abstraction;
using Shared.Dtos;

namespace Ecommerce.Extenstions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
