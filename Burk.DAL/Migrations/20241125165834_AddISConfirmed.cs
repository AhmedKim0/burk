using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddISConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "WaitingLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RecordedId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RecordedId",
                table: "Reviews",
                column: "RecordedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_TempUsers_RecordedId",
                table: "Reviews",
                column: "RecordedId",
                principalTable: "TempUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_TempUsers_RecordedId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RecordedId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "RecordedId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Reviews");
        }
    }
}
