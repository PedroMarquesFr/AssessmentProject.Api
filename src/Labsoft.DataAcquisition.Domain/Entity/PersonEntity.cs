using AssessmentProject.Shared;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class PersonEntity
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tipo de Pessoa é obrigatório")]
        public EPersonType PersonType { get; set; }

        [Required(ErrorMessage = "Documento é obrigatório")]
        [RegularExpression(@"^\d{11}$|^\d{14}$", ErrorMessage = "CPF ou CNPJ inválido")]
        public string Document { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        public string Apelido { get; set; }

        [Required(ErrorMessage = "Endereço de cadastro é obrigatório")]
        public string EnderecoCadastro { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        public string? Password { get; set; }

        [Required(ErrorMessage = "Qualificação é obrigatória")]
        public EQualification Qualification { get; set; }

        public ERoles? Role { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PersonEntity(
            Guid id,
            EPersonType personType,
            string document,
            string nome,
            string apelido,
            string enderecoCadastro,
            string email,
            EQualification qualification,
            ERoles role = ERoles.User,
            string password = "notDefinedYet",
            bool isActivated = true
            )
        {
            var CPFSize = 11;
            var CNPJSize = 14;
            if (personType == EPersonType.Fisica && document.Length != CPFSize)
            {
                throw new ArgumentException("CPF inválido");
            }
            if (personType == EPersonType.Juridica && document.Length != CNPJSize)
            {
                throw new ArgumentException("CNPJ inválido");
            }
            Id = id;
            PersonType = personType;
            Document = document;
            Nome = nome;
            Apelido = apelido;
            EnderecoCadastro = enderecoCadastro;
            Email = email;
            Password = password;
            Qualification = qualification;
            Role = role;
            IsActivated = isActivated;
        }
    }
}


