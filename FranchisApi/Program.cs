using FranchiseRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FranchisService.Service;
using FranchisService;
using FranchisService.Helpers;

namespace FranchisApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            ApplyMigrations(app);

            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<RefreshTokenService>();
            services.AddScoped<JwtTokenHelper>();

            // JWT Authentication
            var jwtSettings = configuration.GetSection("Jwt");
            services.AddAuthentication(options =>
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
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });

            // DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<FranchisDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Repositories and services
            services.AddRepositoryServices();
            services.AddDomainServices();

            services.AddControllers();

            // Swagger/OpenAPI
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        private static void ApplyMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FranchisDbContext>();
            db.Database.Migrate();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Global Exception Handling
            app.UseMiddleware<Middleware.ExceptionHandlingMiddleware>();

            app.UseCors(policy =>
            {
                policy.WithOrigins("http://localhost:4200", "http://127.0.0.1:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
