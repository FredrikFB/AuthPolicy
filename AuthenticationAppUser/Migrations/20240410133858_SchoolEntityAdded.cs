using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationAppUser.Migrations
{
    /// <inheritdoc />
    public partial class SchoolEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolEntitySchoolId",
                table: "UserAddresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgNr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolAddresses",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolAddresses", x => new { x.SchoolId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_SchoolAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolAddresses_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_SchoolEntitySchoolId",
                table: "UserAddresses",
                column: "SchoolEntitySchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolAddresses_AddressId",
                table: "SchoolAddresses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Schools_SchoolEntitySchoolId",
                table: "UserAddresses",
                column: "SchoolEntitySchoolId",
                principalTable: "Schools",
                principalColumn: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Schools_SchoolEntitySchoolId",
                table: "UserAddresses");

            migrationBuilder.DropTable(
                name: "SchoolAddresses");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_SchoolEntitySchoolId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "SchoolEntitySchoolId",
                table: "UserAddresses");
        }
    }
}
