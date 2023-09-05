using AssessmentProject.Application.Abstractions;
using AssessmentProject.Domain.CQRS.Commands;
using AssessmentProject.Domain.CQRS.Queries;
using AssessmentProject.Domain.Entity;
using MediatR;
using System.Security.Claims;
using System.Text;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    public class DeactivatePersonCommandHandler : IRequestHandler<DeactivatePersonCommandRequest, DeactivatePersonCommandResponse>
    {
        private readonly IPersonQueryRepository _personQueryRepository;
        private readonly IPersonCommandRepository _personCommandRepository;
        private readonly IJwtProvider _jwtProvider;

        public DeactivatePersonCommandHandler(IPersonQueryRepository personQueryRepository, IPersonCommandRepository personCommandRepository, IJwtProvider jwtProvider)
        {
            _personQueryRepository = personQueryRepository;
            _personCommandRepository = personCommandRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<DeactivatePersonCommandResponse> Handle(DeactivatePersonCommandRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the person from the repository based on the provided PersonId.
            var existingPerson = await _personQueryRepository.Get(request.PersonId);

            if (existingPerson != null)
            {
                // Update the person's status to "deactivated" (or any other desired status).
                existingPerson.IsActivated = false; // Change "Status" to the actual property name in your PersonEntity class.

                // Save the updated person to the repository.
                await _personCommandRepository.Update(existingPerson);

                // Person status updated successfully.
                return new DeactivatePersonCommandResponse(true);
            }
            else
            {
                // Person with the specified PersonId was not found.
                return new DeactivatePersonCommandResponse(false);
            }
        }
    }
}

