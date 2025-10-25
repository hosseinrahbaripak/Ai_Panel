using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixuserRoletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_ParentId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ParentId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ParentId",
                table: "Roles",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Roles_ParentId",
                table: "Roles",
                column: "ParentId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
