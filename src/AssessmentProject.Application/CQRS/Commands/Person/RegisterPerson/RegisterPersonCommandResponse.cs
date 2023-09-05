using AssessmentProject.Domain.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class RegisterPersonCommandResponse
    {
        public readonly PersonEntity Person;

        public RegisterPersonCommandResponse(PersonEntity person)
        {
            Person = person;
        }
    }
}
