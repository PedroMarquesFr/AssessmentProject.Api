using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class Daq
    {
        public readonly Guid Id;
        public readonly Guid AccountId;
        public readonly string Identification;
        public readonly Guid EquipmentId;
        public readonly string EquipmentIdentification;
        public readonly string Host;
        public readonly Guid DaqTypeId;
        public readonly string DaqTypeIdentification;
        public readonly bool RealTime;
        public readonly bool Active;

        public Daq(
            Guid id,
            Guid accountId,
            string identification,
            Guid equipmentId,
            string equipmentIdentification,
            string host,
            Guid daqTypeId,
            string daqTypeIdentification,
            bool realTime,
            bool active)
        {
            Id = id;
            AccountId = accountId;
            Identification = identification;
            EquipmentId = equipmentId;
            EquipmentIdentification = equipmentIdentification;
            Host = host;
            DaqTypeId = daqTypeId;
            DaqTypeIdentification = daqTypeIdentification;
            RealTime = realTime;
            Active = active;
        }
    }
}