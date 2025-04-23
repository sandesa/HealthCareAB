using UserService.Startup;

namespace UserService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddDependencies();

            var app = builder.Build();

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

            app.UseOpenApi();

            app.MapControllers();

            app.Run();
        }
    }
}
