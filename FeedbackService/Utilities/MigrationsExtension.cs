using FeedbackService.Database;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Utilities
{
    public static class MigrationsExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();
                using FeedbackDbContext? context = scope.ServiceProvider.GetService<FeedbackDbContext>();

                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Automatic migration error: {ex.Message}");
            }
        }
    }
}
