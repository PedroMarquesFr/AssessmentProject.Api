using AssessmentProject.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace AssessmentProject.Api.DTOs
{
    public class PersonDto
    {
        [Required]
        public EPersonType PersonType { get; set; }
        [Required]
        public string Document { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Apelido { get; set; }
        [Required]
        public string EnderecoCadastro { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public EQualification Qualification { get; set; }
        [Required]
        public ERoles Role { get; set; }
        public string Password { get; set; }
    }
}
