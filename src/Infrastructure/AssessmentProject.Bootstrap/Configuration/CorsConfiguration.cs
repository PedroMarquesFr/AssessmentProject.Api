using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class CorsConfiguration
    {
        public static IServiceCollection ConfigureCorsServices(
            this IServiceCollection services,
            IConfiguration configuration,
            string corsPolicy)
        {
            services.AddCors(options =>
            {
                string allowedOrigins = configuration["CorsPolicy:AllowedOrigins"] ?? "";
                string[] origins = allowedOrigins.Split(';');
                options.AddPolicy(name: corsPolicy,
                                  policy =>
                                  {
                                      policy.WithOrigins(origins);
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                  });
            });

            return services;
        }
    }
}
