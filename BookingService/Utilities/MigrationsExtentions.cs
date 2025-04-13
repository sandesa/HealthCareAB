using BookingService.Database;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Utilities
{
    public static class MigrationsExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();
                using BookingDbContext? context = scope.ServiceProvider.GetService<BookingDbContext>();

                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Automatic migration error: {ex.Message}");
            }
        }
    }
}
