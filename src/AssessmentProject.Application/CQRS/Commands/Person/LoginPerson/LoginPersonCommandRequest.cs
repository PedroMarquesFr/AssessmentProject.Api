using AssessmentProject.Domain.Entity;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class LoginPersonCommandRequest : IRequest<LoginPersonCommandResponse>
    {
        public readonly string Email;
        public readonly string Password;

        public LoginPersonCommandRequest(string email, string password) 
        {
            Email = email;
            Password = password;
        }
    }
}
