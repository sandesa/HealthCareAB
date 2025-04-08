using UserService.Utilities;

namespace UserService.Startup
{
    public static class AddDbServices
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
