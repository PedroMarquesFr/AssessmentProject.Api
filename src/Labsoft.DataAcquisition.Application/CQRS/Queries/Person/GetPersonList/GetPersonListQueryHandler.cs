using AssessmentProject.Application.Abstractions;
using AssessmentProject.Domain.CQRS.Commands;
using AssessmentProject.Domain.CQRS.Queries;
using AssessmentProject.Domain.Entity;
using MediatR;
using System.Security.Claims;
using System.Text;

namespace AssessmentProject.Application.CQRS.Query.GetPersonList
{
    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQueryRequest, GetPersonListQueryResponse>
    {
        private readonly IPersonQueryRepository _personQueryRepository;

        public GetPersonListQueryHandler(IPersonQueryRepository personQueryRepository)
        {
            _personQueryRepository = personQueryRepository;
        }

        public async Task<GetPersonListQueryResponse> Handle(GetPersonListQueryRequest request, CancellationToken cancellationToken)
        {
            var existingPerson = await _personQueryRepository.GetList();

            if (existingPerson == null) throw new Exception("No Persons registered.");

            return new GetPersonListQueryResponse(existingPerson);

        }
    }
}

