using JournalService.Utilities;

namespace JournalService.Startup
{
    public static class DbServicesDevConfig
    {
        public static async Task UseDbDevServices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
                //await app.AddJournalSeedData();
            }
        }

        //public static async Task AddJournalSeedData(this WebApplication app)
        //{
        //    bool reseed = true;

        //    using var scope = app.Services.CreateScope();
        //    var services = scope.ServiceProvider;
        //    var context = services.GetRequiredService<JournalDbContext>();

        //    await JournalSeedData.InitializeAsync(context, reseed, CancellationToken.None);
        //    scope.Dispose();
        //}
    }
}
