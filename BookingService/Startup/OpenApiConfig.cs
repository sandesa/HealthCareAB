using Scalar.AspNetCore;

namespace BookingService.Startup
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
                        Title = "Booking Service API",
                        Description = """
                        Booking Service API
                        This API is responsible for managing booking data.
                        The API is designed to be used by client applications that require user authentication and management.
                        The API is built using ASP.NET Core and follows RESTful principles.
                        The API is documented using OpenAPI and provides a Scalar UI for easy exploration of the endpoints.
                        The API is secured with JWT authentication and role-based authorization.
                        The API supports the following roles:
                        - Developer: Full access to all endpoints.
                        - Admin: Access to booking management endpoints.
                        - Caregiver: Limited access to booking data.
                        - User: Basic access to booking data.
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
                    options.Title = "Booking Service API";
                    options.Theme = ScalarTheme.Saturn;
                    options.Layout = ScalarLayout.Modern;
                    options.HideClientButton = true;
                });
            }
        }
    }
}
