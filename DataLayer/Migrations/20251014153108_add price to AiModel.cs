using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addpricetoAiModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CachedInputPrice",
                table: "AiModels",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InputPrice",
                table: "AiModels",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OutputPrice",
                table: "AiModels",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AiModels",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CachedInputPrice", "InputPrice", "OutputPrice" },
                values: new object[] { null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CachedInputPrice",
                table: "AiModels");

            migrationBuilder.DropColumn(
                name: "InputPrice",
                table: "AiModels");

            migrationBuilder.DropColumn(
                name: "OutputPrice",
                table: "AiModels");
        }
    }
}
