using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addContracttemplatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractTemplateId",
                table: "TestAiConfigs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FinalResponse",
                table: "TestAiConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ContractTemplateId",
                table: "AiConfigs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContractTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAiConfigs_ContractTemplateId",
                table: "TestAiConfigs",
                column: "ContractTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AiConfigs_ContractTemplateId",
                table: "AiConfigs",
                column: "ContractTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AiConfigs_ContractTemplates_ContractTemplateId",
                table: "AiConfigs",
                column: "ContractTemplateId",
                principalTable: "ContractTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAiConfigs_ContractTemplates_ContractTemplateId",
                table: "TestAiConfigs",
                column: "ContractTemplateId",
                principalTable: "ContractTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiConfigs_ContractTemplates_ContractTemplateId",
                table: "AiConfigs");

            migrationBuilder.DropForeignKey(
                name: "FK_TestAiConfigs_ContractTemplates_ContractTemplateId",
                table: "TestAiConfigs");

            migrationBuilder.DropTable(
                name: "ContractTemplates");

            migrationBuilder.DropIndex(
                name: "IX_TestAiConfigs_ContractTemplateId",
                table: "TestAiConfigs");

            migrationBuilder.DropIndex(
                name: "IX_AiConfigs_ContractTemplateId",
                table: "AiConfigs");

            migrationBuilder.DropColumn(
                name: "ContractTemplateId",
                table: "TestAiConfigs");

            migrationBuilder.DropColumn(
                name: "FinalResponse",
                table: "TestAiConfigs");

            migrationBuilder.DropColumn(
                name: "ContractTemplateId",
                table: "AiConfigs");
        }
    }
}
