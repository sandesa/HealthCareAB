
using GatewayService.Startup;

namespace GatewayService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddDependencies();

            var app = builder.Build();

            app.UseOpenApi();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
