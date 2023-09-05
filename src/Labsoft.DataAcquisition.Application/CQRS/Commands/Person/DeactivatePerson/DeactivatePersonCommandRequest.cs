using AssessmentProject.Domain.Entity;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class DeactivatePersonCommandRequest : IRequest<DeactivatePersonCommandResponse>
    {
        public readonly Guid PersonId;

        public DeactivatePersonCommandRequest(Guid personId) 
        {
            PersonId = personId;
        }
    }
}
