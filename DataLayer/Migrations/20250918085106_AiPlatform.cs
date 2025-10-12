using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AiPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AiPlatformId",
                table: "AiConfigs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AiPlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiPlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AiModelAiPlatform",
                columns: table => new
                {
                    ModelsId = table.Column<int>(type: "int", nullable: false),
                    PlatformsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiModelAiPlatform", x => new { x.ModelsId, x.PlatformsId });
                    table.ForeignKey(
                        name: "FK_AiModelAiPlatform_AiModels_ModelsId",
                        column: x => x.ModelsId,
                        principalTable: "AiModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AiModelAiPlatform_AiPlatforms_PlatformsId",
                        column: x => x.PlatformsId,
                        principalTable: "AiPlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiConfigs_AiPlatformId",
                table: "AiConfigs",
                column: "AiPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_AiModelAiPlatform_PlatformsId",
                table: "AiModelAiPlatform",
                column: "PlatformsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AiConfigs_AiPlatforms_AiPlatformId",
                table: "AiConfigs",
                column: "AiPlatformId",
                principalTable: "AiPlatforms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiConfigs_AiPlatforms_AiPlatformId",
                table: "AiConfigs");

            migrationBuilder.DropTable(
                name: "AiModelAiPlatform");

            migrationBuilder.DropTable(
                name: "AiPlatforms");

            migrationBuilder.DropIndex(
                name: "IX_AiConfigs_AiPlatformId",
                table: "AiConfigs");

            migrationBuilder.DropColumn(
                name: "AiPlatformId",
                table: "AiConfigs");
        }
    }
}
