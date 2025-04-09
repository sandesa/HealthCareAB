using Microsoft.EntityFrameworkCore;
using UserService.Data;
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
            builder.Services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IHashingRepository, HashingRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<Services.UserService>();

            builder.Services.AddScoped<UserMappingService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();

            builder.Services.AddTransient<UserData>();
        }
    }
}
