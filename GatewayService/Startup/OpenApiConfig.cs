using Scalar.AspNetCore;

namespace GatewayService.Startup
{
    public static class OpenApiConfig
    {
        public static void AddOpenApiServices(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, _) =>
                {
                    document.Info = new()
                    {
                        Title = "Gateway Service API",
                        Description = """
                        Gateway Service API
                        This API is responsible for connecting the frontend application with the backend api services.
                        The API is built using ASP.NET Core and follows RESTful principles.
                        The API is documented using OpenAPI and provides a Scalar UI for easy exploration of the endpoints.
                        The connecting services require authentication and authorization to access the endpoints most endpoints.
                        """,
                    };
                    return Task.CompletedTask;
                });
            });
        }

        public static void UseOpenApi(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.Title = "Gateway Service API";
                    options.Theme = ScalarTheme.Saturn;
                    options.Layout = ScalarLayout.Modern;
                    options.HideClientButton = true;
                });
            }
        }
    }
}
