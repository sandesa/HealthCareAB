using SessionService.Utilities;

namespace SessionService.Startup
{
    public static class DbServicesDevConfig
    {
        public static void UseDbDevServices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
            }
        }
    }
}
