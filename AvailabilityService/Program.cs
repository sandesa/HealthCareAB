
using AvailabilityService.Startup;

namespace AvailabilityService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddDependencies();

            var app = builder.Build();

            app.UseOpenApi();

            await app.UseDbDevServices();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
