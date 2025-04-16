using FeedbackService.Startup;

namespace FeedbackService
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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
