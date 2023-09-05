using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class EquipmentTypeList
    {
        public readonly Guid Id;
        public readonly string Identification;
        public readonly bool Active;

        public EquipmentTypeList(
            Guid id,
            string identification,
            bool active
            )
        {
            Id = id;
            Identification = identification;
            Active = active;
        }
    }
}
