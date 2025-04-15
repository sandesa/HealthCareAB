using SessionService.Data;
using SessionService.Database;
using SessionService.Utilities;

namespace SessionService.Startup
{
    public static class DbServicesDevConfig
    {
        public static async Task UseDbDevServices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
                await app.AddSessionSeedData();
            }
        }

        public static async Task AddSessionSeedData(this WebApplication app)
        {
            bool reseed = true;

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<SessionDbContext>();

            await SessionSeedData.InitializeAsync(context, reseed, CancellationToken.None);
            scope.Dispose();
        }
    }
}
