using AssessmentProject.Domain.Entity;

namespace AssessmentProject.Domain.CQRS.Commands
{
    public interface IPersonCommandRepository
    {
        Task<PersonEntity> Create(PersonEntity person);
        Task<PersonEntity> Update(PersonEntity person);
    }
}
