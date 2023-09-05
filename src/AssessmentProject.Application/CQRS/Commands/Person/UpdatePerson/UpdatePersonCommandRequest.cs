using AssessmentProject.Domain.Entity;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class UpdatePersonCommandRequest : IRequest<UpdatePersonCommandResponse>
    {
        public readonly PersonEntity? Person;

        public UpdatePersonCommandRequest(PersonEntity person)
        {
            Person = person;
        }
    }
}
