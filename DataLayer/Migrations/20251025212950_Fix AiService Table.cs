using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAiServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiServices_AiConfigs_AiConfigId",
                table: "AiServices");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AiConfigId",
                table: "AiServices",
                newName: "AiConfigGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_AiServices_AiConfigId",
                table: "AiServices",
                newName: "IX_AiServices_AiConfigGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AiServices_AiConfigGroups_AiConfigGroupId",
                table: "AiServices",
                column: "AiConfigGroupId",
                principalTable: "AiConfigGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiServices_AiConfigGroups_AiConfigGroupId",
                table: "AiServices");

            migrationBuilder.RenameColumn(
                name: "AiConfigGroupId",
                table: "AiServices",
                newName: "AiConfigId");

            migrationBuilder.RenameIndex(
                name: "IX_AiServices_AiConfigGroupId",
                table: "AiServices",
                newName: "IX_AiServices_AiConfigId");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AiServices_AiConfigs_AiConfigId",
                table: "AiServices",
                column: "AiConfigId",
                principalTable: "AiConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
