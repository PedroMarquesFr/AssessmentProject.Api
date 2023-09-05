using AssessmentProject.Data.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class AssessmentProjectDbConfiguration
    {
        public static void AddPostgreSql(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            const string connectionStringName = "AssessmentProjectDb";
            var connectionString = configuration.GetConnectionString(connectionStringName);
            if (connectionString is null)
                throw new Exception($"{connectionStringName} connection string not found.");

            services.AddDbContext<AssessmentProjectDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                //options.UseSnakeCaseNamingConvention();
            });

            services.AddTransient<IDbConnection>(_ =>
                new NpgsqlConnection(connectionString));
        }
    }
}
