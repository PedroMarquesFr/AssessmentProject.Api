using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using AssessmentProject.Domain.CQRS.Queries;
using AssessmentProject.Domain.Entity;
//using AssessmentProject.Data.Entities;
using System.Diagnostics.CodeAnalysis;
using AssessmentProject.Shared;

namespace AssessmentProject.Data.CQRS.Queries
{
    [ExcludeFromCodeCoverage]
    public class PersonQueryRepository : IPersonQueryRepository
    {
        private readonly IDbConnection _dbConnection;

        public PersonQueryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<PersonEntity?> GetPersonByEmail(string email)
        {
            const string query = "SELECT * FROM \"Person\" WHERE \"Email\" = @email";
            var parameters = new { email };

            var personList = await _dbConnection.QueryAsync<Person>(query, parameters);
            var person = personList.FirstOrDefault();
            if (person == null)
            {
                return null;
            }

            var personEntity = new PersonEntity(
                person.Id,
                (EPersonType) person.TypeId, 
                person.Document, 
                person.Name,
                person.Apelido, 
                person.PersonAddress, 
                person.Email, 
                (EQualification)person.RoleId,
                (ERoles)person.RoleId,
                person.Password);

            return personEntity;
        }

        public async Task<PersonEntity?> Get(Guid personId)
        {
            const string query = "SELECT * FROM \"Person\" WHERE \"Id\" = @personId";
            var parameters = new { personId };

            var personModel = await _dbConnection.QueryAsync<Person>(query, parameters);
            var person = personModel.FirstOrDefault();

            if (person == null) { throw new Exception("Person not found."); }

            var personEntity = new PersonEntity(
                person.Id,
                (EPersonType)person.TypeId,
                person.Document,
                person.Name,
                person.Apelido,
                person.PersonAddress,
                person.Email,
                (EQualification)person.RoleId,
                (ERoles)person.RoleId,
                person.Password);
            personEntity.CreatedAt = person.CreatedAt;
            personEntity.UpdatedAt = person.UpdatedAt;

            return personEntity;
        }

        public async Task<IEnumerable<Person>?> GetList()
        {
            const string query = "SELECT * FROM \"Person\"";

            var personEntities = await _dbConnection.QueryAsync<Person>(query);

            return personEntities.Any() ? personEntities : null;
        }
    }
}
