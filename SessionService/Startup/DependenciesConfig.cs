using Microsoft.EntityFrameworkCore;
using SessionService.Database;

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

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddOpenApiServices();
        }
    }
}
