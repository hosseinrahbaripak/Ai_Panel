using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAiServicetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAiChatLogs_AiConfigs_AiConfigId",
                table: "UserAiChatLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserAiChatLogs_AiConfigId",
                table: "UserAiChatLogs");

            migrationBuilder.DropColumn(
                name: "AiConfigId",
                table: "UserAiChatLogs");

            migrationBuilder.AddColumn<int>(
                name: "UserServiceId",
                table: "UserAiChatLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AiServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRecommended = table.Column<bool>(type: "bit", nullable: false),
                    AiConfigId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: true),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiServices_AiConfigs_AiConfigId",
                        column: x => x.AiConfigId,
                        principalTable: "AiConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AiServiceId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: true),
                    UpdateBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserServices_AiServices_AiServiceId",
                        column: x => x.AiServiceId,
                        principalTable: "AiServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserServices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAiChatLogs_UserServiceId",
                table: "UserAiChatLogs",
                column: "UserServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AiServices_AiConfigId",
                table: "AiServices",
                column: "AiConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServices_AiServiceId",
                table: "UserServices",
                column: "AiServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServices_UserId",
                table: "UserServices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAiChatLogs_UserServices_UserServiceId",
                table: "UserAiChatLogs",
                column: "UserServiceId",
                principalTable: "UserServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAiChatLogs_UserServices_UserServiceId",
                table: "UserAiChatLogs");

            migrationBuilder.DropTable(
                name: "UserServices");

            migrationBuilder.DropTable(
                name: "AiServices");

            migrationBuilder.DropIndex(
                name: "IX_UserAiChatLogs_UserServiceId",
                table: "UserAiChatLogs");

            migrationBuilder.DropColumn(
                name: "UserServiceId",
                table: "UserAiChatLogs");

            migrationBuilder.AddColumn<int>(
                name: "AiConfigId",
                table: "UserAiChatLogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAiChatLogs_AiConfigId",
                table: "UserAiChatLogs",
                column: "AiConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAiChatLogs_AiConfigs_AiConfigId",
                table: "UserAiChatLogs",
                column: "AiConfigId",
                principalTable: "AiConfigs",
                principalColumn: "Id");
        }
    }
}
