using Microsoft.EntityFrameworkCore;
using SessionService.Database;
using SessionService.Interfaces;
using SessionService.Repositories;
using SessionService.Services;

namespace SessionService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SessionDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ISessionRepository, SessionRepository>();

            builder.Services.AddScoped<Services.SessionService>();

            builder.Services.AddScoped<SessionMappingService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
