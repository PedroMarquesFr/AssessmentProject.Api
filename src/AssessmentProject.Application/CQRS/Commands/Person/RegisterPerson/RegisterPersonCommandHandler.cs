using AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType;
using AssessmentProject.Domain.CQRS.Commands;
using AssessmentProject.Domain.CQRS.Queries;
using MediatR;

namespace AssessmentProject.Application.CQRS.Commands.Persons.RegisterPerson
{
    public class RegisterPersonCommandHandler : IRequestHandler<RegisterPersonCommandRequest, RegisterPersonCommandResponse>
    {
        private readonly IPersonCommandRepository _personCommandRepository;
        private readonly IPersonQueryRepository _personQueryRepository;

        public RegisterPersonCommandHandler(IPersonCommandRepository personCommandRepository, IPersonQueryRepository personQueryRepository)
        {
            _personCommandRepository = personCommandRepository;
            _personQueryRepository = personQueryRepository;
        }

        public async Task<RegisterPersonCommandResponse> Handle(RegisterPersonCommandRequest request, CancellationToken cancellationToken)
        {
            if(request?.Person == null)
            {
                throw new ArgumentNullException("Person null.");
            }

            await CheckIfPersonAlreadyExist(request.Person.Email);

            var person = await _personCommandRepository.Create(request.Person);
            return new RegisterPersonCommandResponse(person);
        }
        public async Task CheckIfPersonAlreadyExist(string email)
        {
            var doesPersonAlreadyExists = await _personQueryRepository.GetPersonByEmail(email);
            if (doesPersonAlreadyExists != null)
            {
                throw new InvalidOperationException("Person's Email already exists.");
            }
        }
    }
}
