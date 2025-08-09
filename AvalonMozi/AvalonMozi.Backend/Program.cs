
using AvalonMozi.Application;
using AvalonMozi.Domain.Users;
using AvalonMozi.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag.Generation.Processors.Security;
using NSwag;
using AvalonMozi.Factories;

namespace AvalonMozi.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // CORS HERE

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                    TokenDecryptionKey = null
                };
            });
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<AvalonContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //options.EnableSensitiveDataLogging();
            });

            // DI initialize
            builder.Services.AddServices();
            builder.Services.AddFactories();

            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            builder.Services.AddOpenApiDocument(configure =>
            {
                configure.Title = "AvalonMozi.Backend";
                //configure.Version = "v1";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}.",
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<AvalonContext>();

                    DbInitializer.InitializeAsync(context);
                    DbInitializer.TrySeedAsync(context, configuration);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            if (app.Environment.IsDevelopment())
            {
                // Serve the OpenAPI/Swagger document and Swagger UI before CORS, authentication, and authorization
                app.UseOpenApi(); // Serve the OpenAPI/Swagger document
                app.UseSwaggerUi(); // Serve the Swagger UI
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static IConfiguration GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", true, true)
                .AddJsonFile("appsettings.json", false, true);

            return builder.Build();
        }
    }

}
