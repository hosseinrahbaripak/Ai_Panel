using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Live_Book.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class multiProjectSupervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupervisorProfiles_ProjectProfiles_ProjectId",
                table: "SupervisorProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SupervisorProfiles_ProjectId",
                table: "SupervisorProfiles");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "SupervisorProfiles");

            migrationBuilder.CreateTable(
                name: "SupervisorProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: true),
                    UpdateBy = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisorProject_ProjectProfiles_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupervisorProject_SupervisorProfiles_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "SupervisorProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorProject_ProjectId",
                table: "SupervisorProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorProject_SupervisorId",
                table: "SupervisorProject",
                column: "SupervisorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisorProject");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "SupervisorProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorProfiles_ProjectId",
                table: "SupervisorProfiles",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupervisorProfiles_ProjectProfiles_ProjectId",
                table: "SupervisorProfiles",
                column: "ProjectId",
                principalTable: "ProjectProfiles",
                principalColumn: "Id");
        }
    }
}
