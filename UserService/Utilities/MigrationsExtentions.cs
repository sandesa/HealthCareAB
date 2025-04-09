using Microsoft.EntityFrameworkCore;
using UserService.Database;

namespace UserService.Utilities
{
    public static class MigrationsExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();
                using UserDbContext? context = scope.ServiceProvider.GetService<UserDbContext>();

                context?.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Automatic migration error: {ex.Message}");
            }
        }
    }
}
