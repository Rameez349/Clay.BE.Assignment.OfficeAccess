using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessService, UserAccessService>();
            services.AddScoped<IAccessHistoryService, AccessHistoryService>();
        }
    }
}
