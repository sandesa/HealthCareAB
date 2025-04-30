using Scalar.AspNetCore;

namespace LoginService.Startup
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
                        Title = "Login Service API",
                        Description = """
                        Login Service API
                        This API is responsible for managing login AND logout requests.
                        The API sends a POST request to the Session Service to create a new session after successful login attempt.
                        The API is designed to be used by client applications that require user authentication and management.
                        The API is built using ASP.NET Core and follows RESTful principles.
                        The API is documented using OpenAPI and provides a Scalar UI for easy exploration of the endpoints.
                        The API is has one open endpoint (login) and one endpoint secured with JWT authentication (logout).
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
                    options.Title = "Login Service API";
                    options.Theme = ScalarTheme.Saturn;
                    options.Layout = ScalarLayout.Modern;
                    options.HideClientButton = true;
                });
            }
        }
    }
}
