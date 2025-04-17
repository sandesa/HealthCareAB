using AvailabilityService.Data;
using AvailabilityService.Database;
using AvailabilityService.Utilities;

namespace AvailabilityService.Startup
{
    public static class DbServicesDevConfig
    {
        public static async Task UseDbDevServices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
                await app.AddAvailabilitySeedData();
            }
        }

        public static async Task AddAvailabilitySeedData(this WebApplication app)
        {
            bool reseed = true;

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AvailabilityDbContext>();

            await AvailabilitySeedData.InitializeAsync(context, reseed, CancellationToken.None);
            scope.Dispose();
        }
    }
}
