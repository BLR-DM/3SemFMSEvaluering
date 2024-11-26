using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMSExitSlip.DatabaseMigration.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LectureId",
                table: "ExitSlips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "ExitSlips");
        }
    }
}
