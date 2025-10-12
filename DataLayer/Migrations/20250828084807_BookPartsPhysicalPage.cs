using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ai_Panel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookPartsPhysicalPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PagesCount",
                table: "BookParts",
                newName: "PhysicalStartPage");

            migrationBuilder.AddColumn<int>(
                name: "PhysicalEndPage",
                table: "BookParts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhysicalEndPage",
                table: "BookParts");

            migrationBuilder.RenameColumn(
                name: "PhysicalStartPage",
                table: "BookParts",
                newName: "PagesCount");
        }
    }
}
