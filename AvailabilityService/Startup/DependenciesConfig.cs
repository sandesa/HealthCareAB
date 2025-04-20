using AvailabilityService.Database;
using AvailabilityService.Interfaces;
using AvailabilityService.Repositories;
using AvailabilityService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AvailabilityService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AvailabilityDbContext>(options =>
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

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Developer", policy =>
                    policy.RequireRole("Developer"));

                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("Caregiver", policy =>
                    policy.RequireRole("Caregiver"));
            });

            builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();

            builder.Services.AddScoped<AvailabilityMappingService>();

            builder.Services.AddScoped<Services.AvailabilityService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
