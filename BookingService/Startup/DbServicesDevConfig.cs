using BookingService.Data;
using BookingService.Database;
using BookingService.Utilities;

namespace BookingService.Startup
{
    public static class DbServicesDevConfig
    {
        public static async Task UseDbDevServices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
                await app.AddBookingSeedData();
            }
        }

        public static async Task AddBookingSeedData(this WebApplication app)
        {
            bool reseed = true;

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BookingDbContext>();

            await BookingSeedData.InitializeAsync(context, reseed, CancellationToken.None);
            scope.Dispose();
        }
    }
}
