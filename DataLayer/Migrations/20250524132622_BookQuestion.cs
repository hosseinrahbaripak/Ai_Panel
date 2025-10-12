using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Live_Book.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMultipleChoice = table.Column<bool>(type: "bit", nullable: false),
                    QuestionImages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerImages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: true),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookQuestions_BookParts_PartId",
                        column: x => x.PartId,
                        principalTable: "BookParts",
                        principalColumn: "PartId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BookQuestions_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookQuestions_BookId",
                table: "BookQuestions",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookQuestions_PartId",
                table: "BookQuestions",
                column: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookQuestions");
        }
    }
}
