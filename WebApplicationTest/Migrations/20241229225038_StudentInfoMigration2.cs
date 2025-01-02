using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationTest.Migrations
{
    /// <inheritdoc />
    public partial class StudentInfoMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "StudentInformations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "StudentInformations");
        }
    }
}
