using AssessmentProject.Domain.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType
{
    [ExcludeFromCodeCoverage]
    public class LoginPersonCommandResponse
    {
        public readonly string JwtToken;

        public LoginPersonCommandResponse(string jwtToken)
        {
            JwtToken = jwtToken;
        }
    }
}
