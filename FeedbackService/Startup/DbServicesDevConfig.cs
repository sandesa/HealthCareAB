using FeedbackService.Utilities;

namespace FeedbackService.Startup
{
    public static class DbServicesDevConfig
    {
        public static async Task UseDbDevServices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
                //await app.AddFeedbackSeedData();
            }
        }

        //public static async Task AddFeedbackSeedData(this WebApplication app)
        //{
        //    bool reseed = true;

        //    using var scope = app.Services.CreateScope();
        //    var services = scope.ServiceProvider;
        //    var context = services.GetRequiredService<FeedbackDbContext>();

        //    await FeedbackSeedData.InitializeAsync(context, reseed, CancellationToken.None);
        //    scope.Dispose();
        //}
    }
}
