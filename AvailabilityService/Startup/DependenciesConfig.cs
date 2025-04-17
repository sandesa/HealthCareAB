using AvailabilityService.Database;
using AvailabilityService.Interfaces;
using AvailabilityService.Repositories;
using AvailabilityService.Services;
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

            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();

            builder.Services.AddScoped<AvailabilityMappingService>();

            builder.Services.AddScoped<Services.AvailabilityService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
