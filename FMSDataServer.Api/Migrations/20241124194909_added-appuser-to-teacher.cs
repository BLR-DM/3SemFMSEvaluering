using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMSDataServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedappusertoteacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teachers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_AspNetUsers_AppUserId",
                table: "Teachers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_AspNetUsers_AppUserId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Teachers");
        }
    }
}
