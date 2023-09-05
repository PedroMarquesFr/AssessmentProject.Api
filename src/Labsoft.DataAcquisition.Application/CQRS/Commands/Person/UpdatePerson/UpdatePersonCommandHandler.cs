using AssessmentProject.Application.Abstractions;
using AssessmentProject.Domain.CQRS.Commands;
using AssessmentProject.Domain.CQRS.Queries;
using AssessmentProject.Domain.Entity;
using MediatR;
using System.Security.Claims;
using System.Text;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommandRequest, UpdatePersonCommandResponse>
    {
        private readonly IPersonQueryRepository _personQueryRepository;
        private readonly IPersonCommandRepository _personCommandRepository;
        private readonly IJwtProvider _jwtProvider;

        public UpdatePersonCommandHandler(IPersonQueryRepository personQueryRepository, IPersonCommandRepository personCommandRepository, IJwtProvider jwtProvider)
        {
            _personQueryRepository = personQueryRepository;
            _personCommandRepository = personCommandRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<UpdatePersonCommandResponse> Handle(UpdatePersonCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Person == null) throw new Exception("Person not defined.");
            if (request.Person?.Id == null) throw new Exception("Id not Passed.");

            var existingPerson = await _personQueryRepository.Get(request.Person.Id);

            if (existingPerson == null) throw new Exception("Person doenst exist.");

            existingPerson.IsActivated = false;

            var newPerson = await _personCommandRepository.Update(request.Person);

            return new UpdatePersonCommandResponse(newPerson);

        }
    }
}

