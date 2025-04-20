using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]!);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtConfig:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });


            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IHashingRepository, HashingRepository>();

            builder.Services.AddScoped<IValidationRepository, ValidationRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<Services.UserService>();

            builder.Services.AddScoped<UserMappingService>();

            builder.Services.AddScoped<JwtTokenService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
