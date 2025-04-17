using AvailabilityService.Database;
using Microsoft.EntityFrameworkCore;

namespace AvailabilityService.Utilities
{
    public static class MigrationsExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();
                using AvailabilityDbContext? context = scope.ServiceProvider.GetService<AvailabilityDbContext>();

                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Automatic migration error: {ex.Message}");
            }
        }
    }
}
