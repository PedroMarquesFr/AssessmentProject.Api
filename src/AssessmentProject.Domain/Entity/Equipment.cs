using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class Equipment
    {
        public readonly Guid Id;
        public readonly string? Identification;
        public bool Active { get; set; } //it isn't readonly in order to the Activate and Deactivate functions to be possible using.
        public readonly Guid AccountId;
        public readonly Guid EquipmentTypeId;
        public readonly Guid? EditionUserId;
        public readonly DateTime? EditionDateTime;
        public readonly Guid? ActivationUserId;
        public readonly DateTime? ActivationDateTime;

        public Equipment(
            Guid id,
            string? identification,
            bool active,
            Guid accountId,
            Guid equipmentTypeId,
            Guid? editionUserId,
            DateTime? editionDateTime,
            Guid? activationUserId,
            DateTime? activationDateTime)
        {
            Id = id;
            Identification = identification;
            Active = active;
            AccountId = accountId;
            EquipmentTypeId = equipmentTypeId;
            EditionUserId = editionUserId;
            EditionDateTime = editionDateTime;
            ActivationUserId = activationUserId;
            ActivationDateTime = activationDateTime;
        }
    }
}
