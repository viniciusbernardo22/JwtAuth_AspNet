using JwtAspNet.Services;

namespace JwtAspNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureApp(app);

            app.MapControllers();
            app.Run();
        }

         static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
        }

        static void ConfigureApp(IApplicationBuilder app) 
        {
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

        }
    }
}
