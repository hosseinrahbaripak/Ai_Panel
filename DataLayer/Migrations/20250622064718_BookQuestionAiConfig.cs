using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookQuestionAiConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "UserAiChatLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AiConfigId",
                table: "BookQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAiChatLogs_QuestionId",
                table: "UserAiChatLogs",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookQuestions_AiConfigId",
                table: "BookQuestions",
                column: "AiConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookQuestions_AiConfigs_AiConfigId",
                table: "BookQuestions",
                column: "AiConfigId",
                principalTable: "AiConfigs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAiChatLogs_BookQuestions_QuestionId",
                table: "UserAiChatLogs",
                column: "QuestionId",
                principalTable: "BookQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookQuestions_AiConfigs_AiConfigId",
                table: "BookQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAiChatLogs_BookQuestions_QuestionId",
                table: "UserAiChatLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserAiChatLogs_QuestionId",
                table: "UserAiChatLogs");

            migrationBuilder.DropIndex(
                name: "IX_BookQuestions_AiConfigId",
                table: "BookQuestions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "UserAiChatLogs");

            migrationBuilder.DropColumn(
                name: "AiConfigId",
                table: "BookQuestions");
        }
    }
}
