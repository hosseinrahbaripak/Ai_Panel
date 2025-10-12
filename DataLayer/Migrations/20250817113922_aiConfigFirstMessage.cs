using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Live_Book.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class aiConfigFirstMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstMessage",
                table: "AiConfigs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />  
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstMessage",
                table: "AiConfigs");
        }
    }
}
