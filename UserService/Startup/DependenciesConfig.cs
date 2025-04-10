using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Interfaces;
using UserService.Repositories;
using UserService.Services;

namespace UserService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != "true")
            {
                builder.Services.AddDbContext<UserDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            }
            else
            {
                builder.Services.AddDbContext<UserDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("TestingConnection"),
                        options =>
                        {
                            options.EnableRetryOnFailure();
                        }
                    );
                });
            }

            builder.Services.AddScoped<IHashingRepository, HashingRepository>();

            builder.Services.AddScoped<IValidationRepository, ValidationRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<Services.UserService>();

            builder.Services.AddScoped<UserMappingService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
