using AssessmentProject.Application.Abstractions;
using AssessmentProject.Bootstrap.Configuration;
using AssessmentProject.Bootstrap.Factories;
using AssessmentProject.Bootstrap.Middlewares;
using AssessmentProject.Data.DatabaseContext;
using AssessmentProject.Services.Authentication;
using AssessmentProject.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace AssessmentProject.Service.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string CorsPolicy = "corsPolicy";

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(
            IServiceCollection services)
        {
            SetConfigureServices(services);
        }

        protected virtual void SetConfigureServices(
            IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            LogConfiguration.AddSerilogAssessmentProjectApplicationLog(services,
                configuration: _configuration,
                environment: _webHostEnvironment.EnvironmentName);

            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddEndpointsApiExplorer();
            services.ConfigureDependencyInjectionToTheClasses();
            services.AddSingleton<SqlConnectionFactory>();
            services.AddPostgreSql(_configuration);
            services.ConfigureRepositoryServices();
            services.ConfigureMediatorServices();
            services.ConfigureCorsServices(_configuration, CorsPolicy);
            services.ConfigureSwaggerGenServices(_configuration);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
           AssessmentProjectDbContext dbContext)
        {
            SetAppConfigure(app, env);
            ExecuteMigration(dbContext);
        }

        protected virtual void SetAppConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                Log.Information("Server starting...");

                app.UseMiddleware<ErrorHandlingMiddleware>();
                app.UseMiddleware<RequestSerilogMiddleware>();
                app.UseCors(CorsPolicy);

                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

                Log.Information("Server started...");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Server terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        protected virtual void ExecuteMigration(AssessmentProjectDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }
    }
}
