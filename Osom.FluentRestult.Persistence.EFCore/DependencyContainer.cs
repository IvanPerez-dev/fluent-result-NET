using Osom.FluentRestult.Domain.Persistence;
using Osom.FluentRestult.Persistence.EFCore.Contexts;
using Osom.FluentRestult.Persistence.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Osom.FluentRestult.Persistence.EFCore
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<OsomDbContext>(options =>
                options.UseInMemoryDatabase("OsomDbContext")
            );
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
