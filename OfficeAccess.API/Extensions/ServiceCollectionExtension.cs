using Application.Interfaces;
using Application.Services;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Options;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace OfficeAccess.API.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        ConfigureApplicationServices(services);
        ConfigureDbServices(services, config);
    }

    public static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IUserAccessService, UserAccessService>();
        services.AddScoped<IAccessHistoryService, AccessHistoryService>();
    }

    public static void ConfigureDbServices(this IServiceCollection services, IConfiguration config)
    {
        ConfigureOptions(services, config);
        ConfigureDbContext(services);
        ConfigureRepositories(services);
        ConfigureMigrations(services);
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

    private static void ConfigureMigrations(IServiceCollection services)
    {
        var scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
        var scope = scopeFactory.CreateScope();
        var provider = scope.ServiceProvider;

        using (var migrationDbContext = provider.GetRequiredService<OfficeAccessDbContext>())
        {
            migrationDbContext.Database.SetCommandTimeout(300);
            migrationDbContext.Database.Migrate();
        }
    }
}
