using AssessmentProject.Data.DatabaseContext;
using AssessmentProject.Domain.CQRS.Commands;
using AssessmentProject.Domain.CQRS.Queries;
using AssessmentProject.Domain.Entity;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace AssessmentProject.Data.Repositories
{
    public class PersonCommandRepository : IPersonCommandRepository
    {
        private readonly AssessmentProjectDbContext _dbContext;

        public PersonCommandRepository(AssessmentProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PersonEntity> Create(PersonEntity person)
        {
            // Set the CreatedAt and UpdatedAt properties.
            var dateNow = DateTime.UtcNow.ToUniversalTime();
            var modelPerson = new Person
            {
                Id = person.Id,
                Email = person.Email,
                Document = person.Document,
                Name = person.Nome,
                Password = person.Password,
                TypeId = (int)person.PersonType,
                RoleId = (int)person.Role,
                QualificationId = (int)person.Qualification,
                Apelido = person.Apelido,
                PersonAddress = person.EnderecoCadastro,
                IsActivated = person.IsActivated,
                CreatedAt = dateNow,
                UpdatedAt = dateNow

            };
            _dbContext.Person.Add(modelPerson);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<PersonEntity> Update(PersonEntity person)
        {
            // Set the UpdatedAt property.
            person.UpdatedAt = DateTime.UtcNow;
            var modelPerson = new Person
            {
                Id = person.Id,
                Email = person.Email,
                Name = person.Nome,
                Password = person.Password,
                Document = person.Document,
                TypeId = (int)person.PersonType,
                RoleId = (int)person.Role,
                QualificationId = (int)person.Qualification,
                PersonAddress = person.EnderecoCadastro,
                Apelido = person.Apelido,
                IsActivated = person.IsActivated,
                CreatedAt = person.CreatedAt,
                UpdatedAt = DateTime.UtcNow.ToUniversalTime()

            };
            _dbContext.Update(modelPerson);
            await _dbContext.SaveChangesAsync();

            return person;
        }
    }
}
