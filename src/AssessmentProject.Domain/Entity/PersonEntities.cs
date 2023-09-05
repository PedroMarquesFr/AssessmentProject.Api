using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Domain.Entity
{
    [ExcludeFromCodeCoverage]
    public class Person
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
        public string Apelido { get; set; }
        public int TypeId { get; set; }
        public int RoleId { get; set; }
        public int QualificationId { get; set; }
        public PersonType PersonType { get; set; }
        public string PersonAddress { get; set; }
        public PersonRole? PersonRole { get; set; }
        public PersonQualification? PersonQualification { get; set; }
        public ICollection<Department>? Departments { get; set; }
        public bool IsActivated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class PersonType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Person>? Persons { get; set; }
    }

    public class PersonQualification
    {
        public int Id { get; set; }
        public string QualificationName { get; set; }
        public ICollection<Person>? Persons { get; set; }
    }

    public class PersonRole
    {
        public int Id { get; set; }
        public string RoleType { get; set; }
        public ICollection<Person>? Persons { get; set; }
    }

    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
