using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAiConfiggroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AiConfigGroupId",
                table: "AiConfigs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AiConfigOrder",
                table: "AiConfigs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AiConfigGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiConfigGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiConfigs_AiConfigGroupId",
                table: "AiConfigs",
                column: "AiConfigGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AiConfigs_AiConfigGroups_AiConfigGroupId",
                table: "AiConfigs",
                column: "AiConfigGroupId",
                principalTable: "AiConfigGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiConfigs_AiConfigGroups_AiConfigGroupId",
                table: "AiConfigs");

            migrationBuilder.DropTable(
                name: "AiConfigGroups");

            migrationBuilder.DropIndex(
                name: "IX_AiConfigs_AiConfigGroupId",
                table: "AiConfigs");

            migrationBuilder.DropColumn(
                name: "AiConfigGroupId",
                table: "AiConfigs");

            migrationBuilder.DropColumn(
                name: "AiConfigOrder",
                table: "AiConfigs");
        }
    }
}
