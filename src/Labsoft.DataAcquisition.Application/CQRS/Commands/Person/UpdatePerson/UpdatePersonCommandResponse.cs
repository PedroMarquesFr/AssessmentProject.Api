using AssessmentProject.Domain.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class UpdatePersonCommandResponse
    {
        public readonly PersonEntity Person;

        public UpdatePersonCommandResponse(PersonEntity person)
        {
            Person = person;
        }
    }
}
