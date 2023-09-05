using AssessmentProject.Bootstrap.Factories;
//using AssessmentProject.Data.CQRS.Queries;
using AssessmentProject.Domain.CQRS.Queries;
using AssessmentProject.Domain.Exceptions;
using AssessmentProject.Shared.Constants;
using AssessmentProject.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using AssessmentProject.Domain.CQRS.Commands;
using AssessmentProject.Data.Repositories;
using AssessmentProject.Data.CQRS.Queries;
//using AssessmentProject.Data.CQRS.Commands;

namespace AssessmentProject.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureRepositoryServices(
            this IServiceCollection services)
        {
            #region QueryRepositoriesConfiguration
            services.AddScoped<IPersonQueryRepository, PersonQueryRepository>(
                (servicesProvider) =>
                {
                    var connectionFactory = servicesProvider.GetService<SqlConnectionFactory>();
                    if (connectionFactory is null)
                        throw new AssessmentProjectException(
                            errorDescription: AssessmentProjectErrorMessages.SqlConnectionFactoryNotCreatedError,
                            errorCode: EAssessmentProjectErrorCode.ConnectionDataBaseError);

                    IDbConnection dbConnection = connectionFactory.Create();
                    return new PersonQueryRepository(dbConnection);
                });
            //services.AddScoped<IEquipmentQueryRepository, EquipmentQueryRepository>(
            //    (servicesProvider) =>
            //    {
            //        var connectionFactory = servicesProvider.GetService<SqlConnectionFactory>();
            //        if (connectionFactory is null)
            //            throw new AssessmentProjectException(
            //                errorDescription: AssessmentProjectErrorMessages.SqlConnectionFactoryNotCreatedError,
            //                errorCode: EAssessmentProjectErrorCode.ConnectionDataBaseError);

            //        IDbConnection dbConnection = connectionFactory.Create();
            //        return new EquipmentQueryRepository(dbConnection);
            //    });

            //services.AddScoped<IEquipmentTypeQueryRepository, EquipmentTypeQueryRepository>(
            //    (servicesProvider) =>
            //    {
            //        var connectionFactory = servicesProvider.GetService<SqlConnectionFactory>();
            //        if (connectionFactory is null)
            //            throw new AssessmentProjectException(
            //                errorDescription: AssessmentProjectErrorMessages.SqlConnectionFactoryNotCreatedError,
            //                errorCode: EAssessmentProjectErrorCode.ConnectionDataBaseError);

            //        IDbConnection dbConnection = connectionFactory.Create();
            //        return new EquipmentTypeQueryRepository(dbConnection);
            //    });

            //services.AddScoped<IDaqConfigComQueryRepository, DaqConfigComQueryRepository>(
            //    (servicesProvider) =>
            //    {
            //        var connectionFactory = servicesProvider.GetService<SqlConnectionFactory>();
            //        if (connectionFactory is null)
            //            throw new AssessmentProjectException(
            //                errorDescription: AssessmentProjectErrorMessages.SqlConnectionFactoryNotCreatedError,
            //                errorCode: EAssessmentProjectErrorCode.ConnectionDataBaseError);

            //        IDbConnection dbConnection = connectionFactory.Create();
            //        return new DaqConfigComQueryRepository(dbConnection);
            //    });

            //services.AddScoped<IDaqConfigFileQueryRepository, DaqConfigFileQueryRepository>(
            //    (servicesProvider) =>
            //    {
            //        var connectionFactory = servicesProvider.GetService<SqlConnectionFactory>();
            //        if (connectionFactory is null)
            //            throw new AssessmentProjectException(
            //                errorDescription: AssessmentProjectErrorMessages.SqlConnectionFactoryNotCreatedError,
            //                errorCode: EAssessmentProjectErrorCode.ConnectionDataBaseError);

            //        IDbConnection dbConnection = connectionFactory.Create();
            //        return new DaqConfigFileQueryRepository(dbConnection);
            //    });
            #endregion

            #region CommandRepositoriesConfiguration
            services.AddScoped<IPersonCommandRepository, PersonCommandRepository>();
            //services.AddScoped<IPersonCommandRepository, PersonCommandRepository>();
            //services.AddScoped<IEquipmentCommandRepository, EquipmentCommandRepository>();
            //services.AddScoped<IEquipmentTypeCommandRepository, EquipmentTypeCommandRepository>();
            //services.AddScoped<IDaqConfigComCommandRepository, DaqConfigComCommandRepository>();
            //services.AddScoped<IDaqConfigFileCommandRepository, DaqConfigFileCommandRepository>();
            #endregion

            return services;
        }

    }
}
