namespace GatewayService.Startup
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

            builder.Services.AddHttpClient("LoginService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5148/api/login/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddHttpClient("JournalService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5287/api/journal/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddHttpClient("FeedbackService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5197/api/feedback/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddHttpClient("BookingService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5256/api/booking/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddHttpClient("AvailabilityService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5043/api/availability/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontendApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

            builder.Services.AddControllers();

            builder.Services.AddOpenApiServices();
        }
    }
}
