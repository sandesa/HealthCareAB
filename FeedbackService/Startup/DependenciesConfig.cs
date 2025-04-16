using FeedbackService.Database;
using FeedbackService.Interfaces;
using FeedbackService.Repositories;
using FeedbackService.Services;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<FeedbackDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            builder.Services.AddScoped<Services.FeedbackService>();

            builder.Services.AddScoped<FeedbackMappingService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
