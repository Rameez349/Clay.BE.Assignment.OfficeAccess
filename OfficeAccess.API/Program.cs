using Microsoft.AspNetCore.Authorization;
using OfficeAccess.API.Extensions;
using OfficeAccess.API.Middlewares;

namespace OfficeAccess.API
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.ConfigureJwtService(builder.Configuration);

            builder.Services.ConfigureSwagger();

            builder.Services.ConfigureApplicationServices();

            builder.Services.ConfigureDbServices(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("HistoryAccess", x => x.RequireClaim("CanViewHistory", "True"));

                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}