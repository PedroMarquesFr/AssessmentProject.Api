using AssessmentProject.Domain.Entity;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Application.CQRS.Query.GetPersonList
{
    [ExcludeFromCodeCoverage]
    public class GetPersonListQueryRequest : IRequest<GetPersonListQueryResponse>
    {

        public GetPersonListQueryRequest()
        {
        }
    }
}
