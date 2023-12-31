using DocsHub.Core.Repositories.Interfaces;
using DocsHub.Core.Services;
using DocsHub.Core.Services.Interfaces;
using DocsHub.Infra.Database;
using DocsHub.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocsHub.WebAPI
{
    public static class Configuration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DocsHubPgContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DocsHubPgContext")));
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
