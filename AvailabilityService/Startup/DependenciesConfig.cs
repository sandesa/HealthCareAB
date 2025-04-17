using AvailabilityService.Database;
using Microsoft.EntityFrameworkCore;

namespace AvailabilityService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AvailabilityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
