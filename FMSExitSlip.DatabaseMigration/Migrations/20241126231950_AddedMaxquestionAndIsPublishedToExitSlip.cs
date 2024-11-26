using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMSExitSlip.DatabaseMigration.Migrations
{
    /// <inheritdoc />
    public partial class AddedMaxquestionAndIsPublishedToExitSlip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "ExitSlips",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxQuestions",
                table: "ExitSlips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "ExitSlips");

            migrationBuilder.DropColumn(
                name: "MaxQuestions",
                table: "ExitSlips");
        }
    }
}
