using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Live_Book.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migUpdateAiConfigRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AiConfigs_CreateBy",
                table: "AiConfigs",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_AiConfigs_UpdateBy",
                table: "AiConfigs",
                column: "UpdateBy");

            migrationBuilder.AddForeignKey(
                name: "FK_AiConfigs_AdminLogins_CreateBy",
                table: "AiConfigs",
                column: "CreateBy",
                principalTable: "AdminLogins",
                principalColumn: "LoginID");

            migrationBuilder.AddForeignKey(
                name: "FK_AiConfigs_AdminLogins_UpdateBy",
                table: "AiConfigs",
                column: "UpdateBy",
                principalTable: "AdminLogins",
                principalColumn: "LoginID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AiConfigs_AdminLogins_CreateBy",
                table: "AiConfigs");

            migrationBuilder.DropForeignKey(
                name: "FK_AiConfigs_AdminLogins_UpdateBy",
                table: "AiConfigs");

            migrationBuilder.DropIndex(
                name: "IX_AiConfigs_CreateBy",
                table: "AiConfigs");

            migrationBuilder.DropIndex(
                name: "IX_AiConfigs_UpdateBy",
                table: "AiConfigs");
        }
    }
}
