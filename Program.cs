using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAspNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            ConfigureMiddleware(builder);
            var app = builder.Build();

            ConfigureApp(app);

            app.MapControllers();
            app.Run();
        }

        static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddControllers();
        }

        static void ConfigureApp(IApplicationBuilder app) 
        {
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

        }
    
        static void ConfigureMiddleware(WebApplicationBuilder builder)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.privateKey);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(k =>
            {
                k.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("author", p => p.RequireRole("author"));
            });
        }
    }
}
