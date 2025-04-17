using JournalService.Database;
using JournalService.Interfaces;
using JournalService.Repositories;
using JournalService.Services;
using Microsoft.EntityFrameworkCore;

namespace JournalService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<JournalDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IJournalRepository, JournalRepository>();

            builder.Services.AddScoped<Services.JournalService>();

            builder.Services.AddScoped<JournalMappingService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
