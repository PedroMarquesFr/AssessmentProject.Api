using AssessmentProject.Domain.Entity;

namespace AssessmentProject.Domain.CQRS.Queries
{
    public interface IDaqConfigComQueryRepository
    {
        Task<DaqConfigCom?> Get(Guid id);
        Task<DaqConfigCom?> GetByDaqId(Guid daqId);
    }
}
