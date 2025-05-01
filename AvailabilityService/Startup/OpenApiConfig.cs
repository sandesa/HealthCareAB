using Scalar.AspNetCore;

namespace AvailabilityService.Startup
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
                        Title = "Availability Service API",
                        Description = """
                        Availability Service API
                        This API is responsible for managing availability data.
                        The API is designed to be used by client applications that require user authentication and management.
                        The API is built using ASP.NET Core and follows RESTful principles.
                        The API is documented using OpenAPI and provides a Scalar UI for easy exploration of the endpoints.
                        The API is secured with JWT authentication and role-based authorization.
                        The API supports the following roles:
                        - Developer: Full access to all endpoints.
                        - Admin: Access to availability management endpoints.
                        - Caregiver: Limited access to availability data.
                        - User: Basic access to availability data.
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
                    options.Title = "Availability Service API";
                    options.Theme = ScalarTheme.Saturn;
                    options.Layout = ScalarLayout.Modern;
                    options.HideClientButton = true;
                });
            }
        }
    }
}
