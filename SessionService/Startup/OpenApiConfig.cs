using Scalar.AspNetCore;

namespace SessionService.Startup
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
                        Title = "Session Service API",
                        Description = """
                        Session Service API
                        This API is responsible for managing session data.
                        The API is designed to be used by client applications that require user authentication and management.
                        The API is built using ASP.NET Core and follows RESTful principles.
                        The API is documented using OpenAPI and provides a Scalar UI for easy exploration of the endpoints.
                        The API is secured with JWT authentication and role-based authorization.
                        The API supports the following roles:
                        - Developer: Full access to all endpoints.
                        - Admin: Access to session management endpoints.
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
                    options.Title = "Session Service API";
                    options.Theme = ScalarTheme.Saturn;
                    options.Layout = ScalarLayout.Modern;
                    options.HideClientButton = true;
                });
            }
        }
    }
}
