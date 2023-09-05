using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class EquipmentList
    {
        public readonly Guid Id;
        public readonly string Identification;
        public readonly string Type;
        public readonly bool Active;

        public EquipmentList(
            Guid id,
            string identification,
            string type,
            bool active)
        {
            Id = id; 
            Identification = identification;    
            Type = type;    
            Active = active;    
        }
    }
}
