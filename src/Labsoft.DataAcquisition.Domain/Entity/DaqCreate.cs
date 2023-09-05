using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class DaqCreate
    {
        public readonly Guid? Id;
        public readonly string Identification;
        public readonly bool Active;
        public readonly Guid AccountId;
        public readonly Guid EquipmentId;
        public readonly Guid DaqTypeId;
        public readonly bool RealTime;
        public readonly string Host;
        public readonly Guid UserId;

        public DaqCreate(
            Guid? id,
            string identification,
            bool active,
            Guid accountId,
            Guid equipmentId,
            Guid daqTypeId,
            bool realTime,
            string host,
            Guid userId)
        {
            Id = id;
            Identification = identification;
            Active = active;
            AccountId = accountId;
            EquipmentId = equipmentId;
            DaqTypeId = daqTypeId;
            RealTime = realTime;
            Host = host;
            UserId = userId;
        }

    }
}
