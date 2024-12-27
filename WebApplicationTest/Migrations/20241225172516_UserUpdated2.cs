using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationTest.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileChangeFileName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "no-image-icon-512x512-lfoanl0w.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileChangeFileName",
                table: "Users");
        }
    }
}
