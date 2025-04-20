using FeedbackService.Database;
using FeedbackService.Interfaces;
using FeedbackService.Repositories;
using FeedbackService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FeedbackService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<FeedbackDbContext>(options =>
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

            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            builder.Services.AddScoped<Services.FeedbackService>();

            builder.Services.AddScoped<FeedbackMappingService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
