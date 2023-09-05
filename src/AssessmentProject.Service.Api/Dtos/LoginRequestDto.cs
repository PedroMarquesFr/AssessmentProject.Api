using System.ComponentModel.DataAnnotations;

namespace AssessmentProject.Service.Api.Dtos
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; }
        [Required]
        public string Password { get; }

        public LoginRequestDTO(
            string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
