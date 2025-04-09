using UserService.Data;
using UserService.Database;
using UserService.Interfaces;
using UserService.Utilities;

namespace UserService.Startup
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

        public static async Task AddUserSeedData(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                bool reseed = true;

                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<UserDbContext>();
                var hashingRepository = services.GetRequiredService<IHashingRepository>();

                await UserSeedData.InitializeAsync(context, reseed, CancellationToken.None, hashingRepository);
                scope.Dispose();
            }
        }
    }
}
