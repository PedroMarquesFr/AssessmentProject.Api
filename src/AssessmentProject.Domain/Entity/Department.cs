namespace AssessmentProject.Domain.Entity
{
    public class Department1
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid PersonReponsible { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Constructor to initialize properties
        public Department1(Guid id, string name, Guid personReponsible)
        {
            Id = id;
            Name = name;
            PersonReponsible = personReponsible;
        }
    }

}
