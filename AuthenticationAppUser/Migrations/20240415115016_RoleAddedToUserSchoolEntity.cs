using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationAppUser.Migrations
{
    /// <inheritdoc />
    public partial class RoleAddedToUserSchoolEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserSchools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserSchools");
        }
    }
}
