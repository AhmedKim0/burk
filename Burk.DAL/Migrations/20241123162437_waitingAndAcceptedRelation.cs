using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class waitingAndAcceptedRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WaitingListId",
                table: "AceeptedUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AceeptedUsers_WaitingListId",
                table: "AceeptedUsers",
                column: "WaitingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AceeptedUsers_WaitingLists_WaitingListId",
                table: "AceeptedUsers",
                column: "WaitingListId",
                principalTable: "WaitingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AceeptedUsers_WaitingLists_WaitingListId",
                table: "AceeptedUsers");

            migrationBuilder.DropIndex(
                name: "IX_AceeptedUsers_WaitingListId",
                table: "AceeptedUsers");

            migrationBuilder.DropColumn(
                name: "WaitingListId",
                table: "AceeptedUsers");
        }
    }
}
