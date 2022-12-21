using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Options;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.ServiceExtensions
{
    public static class DbServiceExtensions
    {
        public static void AddDbServices(this IServiceCollection services, IConfiguration config)
        {
            ConfigureOptions(services, config);
            ConfigureDbContext(services);
            ConfigureRepositories(services);
        }

        private static void ConfigureOptions(IServiceCollection services, IConfiguration config)
        {
            services.Configure<DbOptions>(opt => config.GetSection(DbOptions.Key).Bind(opt));
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserAccessRepository, UserAccessRepository>();
            services.AddScoped<IAccessHistoryRepository, AccessHistoryRepository>();
        }

        private static void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<OfficeAccessDbContext>(options => options.UseSqlServer());
        }
    }
}
