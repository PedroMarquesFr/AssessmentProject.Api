//using AssessmentProject.Data.Entities;
using AssessmentProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace AssessmentProject.Data.DatabaseContext
{
    [ExcludeFromCodeCoverage]
    public class AssessmentProjectDbContext : DbContext 
    {
        public AssessmentProjectDbContext()
        {
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonType> PersonType { get; set; }
        public DbSet<PersonRole> PersonRole { get; set; }
        public DbSet<Department> Department { get; set; }

        public AssessmentProjectDbContext(DbContextOptions<AssessmentProjectDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=YourDbName0;Username=postgres;Password=postgres;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<PersonType>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<PersonRole>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<PersonQualification>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Department>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Person>()
                .HasOne(i => i.PersonType)
                .WithMany(i => i.Persons)
                .HasForeignKey(b => b.TypeId);

            modelBuilder.Entity<Person>()
                .HasOne(i => i.PersonRole)
                .WithMany(i => i.Persons)
                .HasForeignKey(b => b.RoleId);
            modelBuilder.Entity<Person>()
                .HasOne(i => i.PersonQualification)
                .WithMany(i => i.Persons)
                .HasForeignKey(b => b.QualificationId);

            //// Seed data for PersonType
            //modelBuilder.Entity<PersonType>().HasData(
            //    new PersonType { Id = 1, TypeName = "Fisica" },
            //    new PersonType { Id = 2, TypeName = "Juridica" }
            //);

            //// Seed data for PersonQualification
            //modelBuilder.Entity<PersonQualification>().HasData(
            //    new PersonQualification { Id = 1, QualificationName = "Cliente" },
            //    new PersonQualification { Id = 2, QualificationName = "Fornecedor" },
            //    new PersonQualification { Id = 3, QualificationName = "Colaborador" }
            //);

            //// Seed data for PersonRole
            //modelBuilder.Entity<PersonRole>().HasData(
            //    new PersonRole { Id = 1, RoleType = "User" },
            //    new PersonRole { Id = 2, RoleType = "Admin" }
            //);

            //// Seed data for Person
            //modelBuilder.Entity<Person>().HasData(
            //    new Person
            //    {
            //        Id = new Guid("b407315e-e24d-4a8a-ba35-22fbf815011a"),
            //        Email = "admin@admin.com",
            //        Name = "Admin",
            //        Password = "admin",
            //        Document = "48750168088",
            //        Apelido = "PersonOne",
            //        TypeId = 1, // Assign a valid TypeId here
            //        RoleId = 2, // Assign a valid RoleId here
            //        QualificationId = 3, // Assign a valid QualificationId here
            //        PersonAddress = "{\r\n  \"cep\": \"62040-020\",\r\n  \"logradouro\": \"Rua Raimundo Mendes Aguiar\",\r\n  \"complemento\": \"\",\r\n  \"bairro\": \"Coração de Jesus\",\r\n  \"localidade\": \"Sobral\",\r\n  \"uf\": \"CE\",\r\n  \"ibge\": \"2312908\",\r\n  \"gia\": \"\",\r\n  \"ddd\": \"88\",\r\n  \"siafi\": \"1559\"\r\n}",
            //        IsActivated = true,
            //        CreatedAt = DateTime.UtcNow,
            //        UpdatedAt = DateTime.UtcNow
            //    },
            //    new Person
            //    {
            //        Id = Guid.NewGuid(),
            //        Email = "person1@example.com",
            //        Name = "Person 1",
            //        Password = "password1",
            //        Document = "82416611003",
            //        Apelido = "PersonTwo",
            //        TypeId = 1, // Assign a valid TypeId here
            //        RoleId = 1, // Assign a valid RoleId here
            //        QualificationId = 3, // Assign a valid QualificationId here
            //        PersonAddress = "{\r\n  \"cep\": \"62040-020\",\r\n  \"logradouro\": \"Rua Raimundo Mendes Aguiar\",\r\n  \"complemento\": \"\",\r\n  \"bairro\": \"Coração de Jesus\",\r\n  \"localidade\": \"Sobral\",\r\n  \"uf\": \"CE\",\r\n  \"ibge\": \"2312908\",\r\n  \"gia\": \"\",\r\n  \"ddd\": \"88\",\r\n  \"siafi\": \"1559\"\r\n}",
            //        IsActivated = true,
            //        CreatedAt = DateTime.UtcNow,
            //        UpdatedAt = DateTime.UtcNow
            //    }
            //);


            //// Seed data for Department
            //modelBuilder.Entity<Department>().HasData(
            //    new Department
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Department1",
            //        PersonId = new Guid("b407315e-e24d-4a8a-ba35-22fbf815011a"), 
            //        CreatedAt = DateTime.UtcNow,
            //        UpdatedAt = DateTime.UtcNow
            //    },
            //    new Department
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Department2",
            //        PersonId = new Guid("b407315e-e24d-4a8a-ba35-22fbf815011a"),
            //        CreatedAt = DateTime.UtcNow,
            //        UpdatedAt = DateTime.UtcNow
            //    }
            //);
        }
}
}
