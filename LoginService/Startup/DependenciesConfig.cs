﻿using LoginService.Interfaces;
using LoginService.Repositores;

namespace LoginService.Startup
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("UserService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5050/api/user/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddHttpClient("SessionService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5142/api/session/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddScoped<ILoginRepository, LoginRepository>();

            builder.Services.AddScoped<Services.LoginService>();

            builder.Services.AddScoped<Services.JwtService>();

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
