using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssessmentProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class GenerateSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PersonQualification",
                columns: new[] { "Id", "QualificationName" },
                values: new object[,]
                {
                    { 1, "Cliente" },
                    { 2, "Fornecedor" },
                    { 3, "Colaborador" }
                });

            migrationBuilder.InsertData(
                table: "PersonRole",
                columns: new[] { "Id", "RoleType" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "PersonType",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Fisica" },
                    { 2, "Juridica" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Apelido", "CreatedAt", "Document", "Email", "IsActivated", "Name", "Password", "PersonAddress", "QualificationId", "RoleId", "TypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b407315e-e24d-4a8a-ba35-22fbf815011a"), "PersonOne", new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8388), "48750168088", "admin@admin.com", true, "Admin", "admin", "{\r\n  \"cep\": \"62040-020\",\r\n  \"logradouro\": \"Rua Raimundo Mendes Aguiar\",\r\n  \"complemento\": \"\",\r\n  \"bairro\": \"Coração de Jesus\",\r\n  \"localidade\": \"Sobral\",\r\n  \"uf\": \"CE\",\r\n  \"ibge\": \"2312908\",\r\n  \"gia\": \"\",\r\n  \"ddd\": \"88\",\r\n  \"siafi\": \"1559\"\r\n}", 3, 2, 1, new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8390) },
                    { new Guid("ffb911ff-5b1a-4b1c-8d79-9f71ab4cb1d6"), "PersonTwo", new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8414), "82416611003", "person1@example.com", true, "Person 1", "password1", "{\r\n  \"cep\": \"62040-020\",\r\n  \"logradouro\": \"Rua Raimundo Mendes Aguiar\",\r\n  \"complemento\": \"\",\r\n  \"bairro\": \"Coração de Jesus\",\r\n  \"localidade\": \"Sobral\",\r\n  \"uf\": \"CE\",\r\n  \"ibge\": \"2312908\",\r\n  \"gia\": \"\",\r\n  \"ddd\": \"88\",\r\n  \"siafi\": \"1559\"\r\n}", 3, 1, 1, new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8414) }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "CreatedAt", "Name", "PersonId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("12e5e015-f8d2-4784-9103-465ba2cfabba"), new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8427), "Department1", new Guid("b407315e-e24d-4a8a-ba35-22fbf815011a"), new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8429) },
                    { new Guid("bbfb2e4c-987a-4935-8803-a14f03a612c1"), new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8430), "Department2", new Guid("b407315e-e24d-4a8a-ba35-22fbf815011a"), new DateTime(2023, 9, 5, 14, 6, 21, 258, DateTimeKind.Utc).AddTicks(8431) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("12e5e015-f8d2-4784-9103-465ba2cfabba"));

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: new Guid("bbfb2e4c-987a-4935-8803-a14f03a612c1"));

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: new Guid("ffb911ff-5b1a-4b1c-8d79-9f71ab4cb1d6"));

            migrationBuilder.DeleteData(
                table: "PersonQualification",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PersonQualification",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PersonType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "Id",
                keyValue: new Guid("b407315e-e24d-4a8a-ba35-22fbf815011a"));

            migrationBuilder.DeleteData(
                table: "PersonRole",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PersonQualification",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PersonRole",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PersonType",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
