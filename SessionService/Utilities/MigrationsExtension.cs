using Microsoft.EntityFrameworkCore;
using SessionService.Database;

namespace SessionService.Utilities
{
    public static class MigrationsExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();
                using SessionDbContext? context = scope.ServiceProvider.GetService<SessionDbContext>();

                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Automatic migration error: {ex.Message}");
            }
        }
    }
}
