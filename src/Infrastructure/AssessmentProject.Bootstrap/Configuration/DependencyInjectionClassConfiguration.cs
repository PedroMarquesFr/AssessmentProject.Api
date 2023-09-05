using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionClassConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjectionToTheClasses(
        this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.TryAddSingleton<IBiKeysService, BiKeysService>();
            //services.AddScoped<IHttpRequestService, ClientHttpRequestService>();
            //services.AddScoped<IDataViewerKeysService, DataViewerKeysService>();
            return services;
        }
    }
}
