using AssessmentProject.Bootstrap.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerGenConfiguration
    {
        private const string SwaggerTitle = "El Shaday API";

        private const string SwaggerVersion = "v1";

        public static IServiceCollection ConfigureSwaggerGenServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerVersion, new OpenApiInfo
                {
                    Title = SwaggerTitle,
                    Version = SwaggerVersion,
                    Description = $"Build: {configuration["BuildNumber"]}"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
                        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                        "Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }
    }
}
