using AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType;
using AssessmentProject.Application.CQRS.Commands.Persons.RegisterPerson;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Bootstrap.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class MediatorConfiguration
    {
        public static IServiceCollection ConfigureMediatorServices(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginPersonCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterPersonCommandHandler).Assembly));
            return services;
        }
    }
}
