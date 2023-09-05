using AssessmentProject.Application.Abstractions;
using AssessmentProject.Domain.Entity;
using AssessmentProject.Shared;
using AssessmentProject.Application.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject.Services.Authentication
{
    public class JwtProvider: IJwtProvider
    {
        public string Generate(PersonEntity person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(person),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddHours(20)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        private static ClaimsIdentity AddClaims(PersonEntity person)
        {
            var claims = new ClaimsIdentity();
            var userRole = person.Role == ERoles.Admin ? "Admin" : "User";

            claims.AddClaim(new Claim(ClaimTypes.Role, userRole));

            return claims;
        }
    }
}
