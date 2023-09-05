using AssessmentProject.Domain.Entity;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class RegisterPersonCommandRequest : IRequest<RegisterPersonCommandResponse>
    {
        public readonly PersonEntity? Person;

        public RegisterPersonCommandRequest(PersonEntity person) 
        {
            Person = person;
        }
    }
}
