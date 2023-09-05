using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Npgsql;

namespace AssessmentProject.Bootstrap.Factories
{
    [ExcludeFromCodeCoverage]
    public class SqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create()
        {
            var connectionString = GetConnectionString();

            return new NpgsqlConnection(connectionString);
        }

        public string GetConnectionString()
        {
            var connectionStringName = $"AssessmentProjectDb";
            var connectionString = _configuration.GetConnectionString(connectionStringName);

            if (connectionString is null)
                throw new Exception($"{connectionStringName} connection string not found.");

            return connectionString;
        }
    }
}
