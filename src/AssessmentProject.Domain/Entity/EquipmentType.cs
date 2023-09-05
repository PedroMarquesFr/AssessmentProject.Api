using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class EquipmentType
    {
        public readonly Guid Id;
        public readonly string? Identification;
        public bool Active { get; set; }
        public readonly Guid AccountId;
        public readonly Guid? EditionUserId;
        public readonly DateTime? EditionDateTime;
        public readonly Guid? ActivationUserId;
        public readonly DateTime? ActivationDateTime;

        public EquipmentType(
            Guid id,
            string? identification,
            bool active,
            Guid accountId,
            Guid? editionUserId,
            DateTime? editionDateTime,
            Guid? activationUserId,
            DateTime? activationDateTime
            )
        {
            Id = id;
            Identification = identification;
            Active = active;
            AccountId = accountId;
            EditionUserId = editionUserId;
            EditionDateTime = editionDateTime;
            ActivationUserId = activationUserId;
            ActivationDateTime = activationDateTime;               
        }
    }
}

