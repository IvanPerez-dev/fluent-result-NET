using Osom.FluentRestult.Application;
using Osom.FluentRestult.Persistence.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace Osom.FluentRestult.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddPersistence();
            services.AddApplicationServices();
            return services;
        }
    }
}
