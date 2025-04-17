using JournalService.Database;
using Microsoft.EntityFrameworkCore;

namespace JournalService.Utilities
{
    public static class MigrationsExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();
                using JournalDbContext? context = scope.ServiceProvider.GetService<JournalDbContext>();

                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Automatic migration error: {ex.Message}");
            }
        }
    }
}
