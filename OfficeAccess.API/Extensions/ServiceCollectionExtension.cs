using Application.Interfaces;
using Application.Services;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Options;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Options;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;

namespace OfficeAccess.API.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureJwtService(this IServiceCollection services, IConfiguration config)
    {
        ConfigureJwtOptions(services, config);
        ConfigureJwtAuthentication(services);
    }

    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDoorsService, DoorsService>();
    }

    public static void ConfigureDbServices(this IServiceCollection services, IConfiguration config)
    {
        ConfigureDbOptions(services, config);
        ConfigureDbContext(services);
        ConfigureRepositories(services);
        ConfigureMigrations(services);
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = $"Specify Authorization token.",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Authorization",
                            Type =  SecuritySchemeType.Http,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Authorization",
                            },
                        },
                        Array.Empty<string>()
                    },
                });
        });
    }

    private static void ConfigureDbOptions(IServiceCollection services, IConfiguration config)
    {
        services.Configure<DbOptions>(config.GetSection(DbOptions.Key));
    }

    private static void ConfigureJwtOptions(IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(opt => config.GetSection(JwtOptions.Key).Bind(opt));
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IDoorsRepository, DoorsRepository>();
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

    private static void ConfigureJwtAuthentication(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var opt = provider.GetRequiredService<IOptions<JwtOptions>>().Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = opt.Issuer,
                ValidAudience = opt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.SecretKey)),
            };
        });
    }

    
}
