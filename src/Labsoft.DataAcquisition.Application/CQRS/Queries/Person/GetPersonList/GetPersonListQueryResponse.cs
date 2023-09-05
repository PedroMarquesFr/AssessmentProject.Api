using AssessmentProject.Domain.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace AssessmentProject.Application.CQRS.Query.GetPersonList
{
    [ExcludeFromCodeCoverage]
    public class GetPersonListQueryResponse
    {
        public readonly IEnumerable<Person> Persons;

        public GetPersonListQueryResponse(IEnumerable<Person> persons)
        {
            Persons = persons;
        }
    }
}
