using AssessmentProject.Application.Abstractions;
using AssessmentProject.Domain.CQRS.Queries;
using AssessmentProject.Domain.Entity;
using MediatR;
using System.Security.Claims;
using System.Text;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    public class LoginPersonCommandHandler : IRequestHandler<LoginPersonCommandRequest, LoginPersonCommandResponse>
    {
        private readonly IPersonQueryRepository _personQueryRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginPersonCommandHandler(IPersonQueryRepository personQueryRepository, IJwtProvider jwtProvider)
        {
            _personQueryRepository = personQueryRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginPersonCommandResponse> Handle(LoginPersonCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Check data
            var person = await _personQueryRepository.GetPersonByEmail(request.Email);

            if (person == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            if (person.Password == null)
            {
                throw new UnauthorizedAccessException("The person itself didnt created the email yet");
            }
            bool isPasswordValid = ValidatePassword(request.Password, person.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // 2. Generate a JWT token
            var token = _jwtProvider.Generate(person);
            return new LoginPersonCommandResponse(token);
        }

        private bool ValidatePassword(string inputPassword, string password)
        {
            if(inputPassword != password)
            {
                return false;
            }
            return true;
        }
    }
}

