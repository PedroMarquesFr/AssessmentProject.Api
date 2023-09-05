using AssessmentProject.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject.Domain.CQRS.Queries
{
    public interface IPersonQueryRepository
    {
        Task<PersonEntity?> GetPersonByEmail(string email);
        Task<PersonEntity?> Get(Guid personId);
        Task<IEnumerable<Person>?> GetList();
    }
}
    