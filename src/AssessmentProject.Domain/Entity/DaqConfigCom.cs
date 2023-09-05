using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class DaqConfigCom
    {
        public readonly Guid? Id;
        public readonly string? Identification;
        public readonly bool? Active;
        public readonly Guid? DaqId;
        public readonly string? PortName;
        public readonly int? BaudRate;
        public readonly int? Parity;
        public readonly int? DataBits;
        public readonly int? StopBit;
        public readonly bool? DtrEnable;
        public readonly bool? RtsEnable;
        public readonly int? ReadInterval;
        public readonly int? Timeout;
        public readonly string? StopText;
        public readonly DateTime? EditionDateTime;
        public readonly Guid? EditionUserId;
        public readonly DateTime? ActivationDateTime;
        public readonly Guid? ActivationUserId;

        public DaqConfigCom(
            Guid? id, 
            string? identification, 
            bool? active, 
            Guid? daqId, 
            string? portName,
            int? baudRate, 
            int? parity, 
            int? dataBits, 
            int? stopBit, 
            bool? dtrEnable,
            bool? rtsEnable, 
            int? readInterval, 
            int? timeout, 
            string? stopText,
            DateTime? editionDateTime, 
            Guid? editionUserId, 
            DateTime? activationDateTime,
            Guid? activationUserId)
        {
            Id = id;
            Identification = identification;
            Active = active;
            DaqId = daqId;
            PortName = portName;
            BaudRate = baudRate;
            Parity = parity;
            DataBits = dataBits;
            StopBit = stopBit;
            DtrEnable = dtrEnable;
            RtsEnable = rtsEnable;
            ReadInterval = readInterval;
            Timeout = timeout;
            StopText = stopText;
            EditionDateTime = editionDateTime;
            EditionUserId = editionUserId;
            ActivationDateTime = activationDateTime;
            ActivationUserId = activationUserId;
        }

    }
}
