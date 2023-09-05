using AssessmentProject.Domain.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class DeactivatePersonCommandResponse
    {
        public readonly bool Deactivated;

        public DeactivatePersonCommandResponse(bool deactivated)
        {
            Deactivated = deactivated;
        }
    }
}
