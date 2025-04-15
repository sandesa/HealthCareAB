using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.RequireHttpsMetadata = false;
                }
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                    ValidAudience = builder.Configuration["JwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["JwtConfig:Secret"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
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
