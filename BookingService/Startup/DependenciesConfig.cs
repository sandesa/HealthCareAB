using BookingService.Database;
using BookingService.Interfaces;
using BookingService.Repositories;
using BookingService.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BookingDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            builder.Services.AddScoped<Services.BookingService>();

            builder.Services.AddScoped<BookingMappingService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
