using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Live_Book.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookCategoryIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "BookCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "BookCategories");
        }
    }
}
