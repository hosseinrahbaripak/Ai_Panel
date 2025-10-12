using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Live_Book.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migAiTestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestAiConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    AiId = table.Column<int>(type: "int", nullable: false),
                    AiModelId = table.Column<int>(type: "int", nullable: false),
                    MaxTokens = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    BookContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    N = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    TopP = table.Column<float>(type: "real", nullable: false),
                    PresencePenalty = table.Column<float>(type: "real", nullable: false),
                    FrequencyPenalty = table.Column<float>(type: "real", nullable: false),
                    Stop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SummarizationCost = table.Column<double>(type: "float", nullable: false),
                    RequestCost = table.Column<double>(type: "float", nullable: false),
                    EmbeddingCost = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: true),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAiConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAiConfigs_AdminLogins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminLogins",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAiConfigs_AiModels_AiId",
                        column: x => x.AiId,
                        principalTable: "AiModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAiConfigs_AiModels_AiModelId",
                        column: x => x.AiModelId,
                        principalTable: "AiModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TestAiConfigs_BookParts_PartId",
                        column: x => x.PartId,
                        principalTable: "BookParts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAiConfigs_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAiConfigs_AdminId",
                table: "TestAiConfigs",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAiConfigs_AiId",
                table: "TestAiConfigs",
                column: "AiId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAiConfigs_AiModelId",
                table: "TestAiConfigs",
                column: "AiModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAiConfigs_BookId",
                table: "TestAiConfigs",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAiConfigs_PartId",
                table: "TestAiConfigs",
                column: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestAiConfigs");
        }
    }
}
