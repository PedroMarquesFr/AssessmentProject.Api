using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class DaqList
    {

        public readonly Guid Id;
        public readonly string Identification;
        public readonly string EquipmentIdentification;
        public readonly string Host;
        public readonly string DaqTypeIdentification;
        public readonly bool RealTime;
        public readonly bool Active;

        public DaqList(
            Guid id,
            string identification,
            string equipmentIdentification,
            string host,
            string daqTypeIdentification,
            bool realTime,
            bool active)
        {
            Id = id;
            Identification = identification;
            EquipmentIdentification = equipmentIdentification;
            Host = host;
            DaqTypeIdentification = daqTypeIdentification;
            RealTime = realTime;
            Active = active;
        }
    }
}
