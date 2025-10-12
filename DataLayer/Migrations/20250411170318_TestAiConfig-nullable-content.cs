using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TestAiConfignullablecontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAiConfigs_BookParts_PartId",
                table: "TestAiConfigs");

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "TestAiConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "TestAiConfigs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BookContent",
                table: "TestAiConfigs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_TestAiConfigs_BookParts_PartId",
                table: "TestAiConfigs",
                column: "PartId",
                principalTable: "BookParts",
                principalColumn: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestAiConfigs_BookParts_PartId",
                table: "TestAiConfigs");

            migrationBuilder.AlterColumn<int>(
                name: "PartId",
                table: "TestAiConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "TestAiConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookContent",
                table: "TestAiConfigs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TestAiConfigs_BookParts_PartId",
                table: "TestAiConfigs",
                column: "PartId",
                principalTable: "BookParts",
                principalColumn: "PartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
