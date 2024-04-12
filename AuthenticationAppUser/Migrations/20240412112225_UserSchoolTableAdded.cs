using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationAppUser.Migrations
{
    /// <inheritdoc />
    public partial class UserSchoolTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_AppUserId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_AppUserId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Schools");

            migrationBuilder.CreateTable(
                name: "UserSchools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchools", x => new { x.SchoolId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSchools_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSchools_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSchools_UserId",
                table: "UserSchools",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSchools");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Schools",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_AppUserId",
                table: "Schools",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_AppUserId",
                table: "Schools",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
