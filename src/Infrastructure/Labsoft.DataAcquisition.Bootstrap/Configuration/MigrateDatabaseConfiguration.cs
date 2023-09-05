using AssessmentProject.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class MigrateDatabaseConfiguration
    {
        public static IServiceProvider MigrateDatabaseOnStartup(
        this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AssessmentProjectDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            return service;
        }
    }
}
