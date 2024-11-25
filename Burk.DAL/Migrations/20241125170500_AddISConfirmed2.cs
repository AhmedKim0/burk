using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddISConfirmed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_TempUsers_RecordedId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TempUsers_Clients_ClientId",
                table: "TempUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TempUsers",
                table: "TempUsers");

            migrationBuilder.RenameTable(
                name: "TempUsers",
                newName: "RecordedVisit");

            migrationBuilder.RenameIndex(
                name: "IX_TempUsers_ClientId",
                table: "RecordedVisit",
                newName: "IX_RecordedVisit_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecordedVisit",
                table: "RecordedVisit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecordedVisit_Clients_ClientId",
                table: "RecordedVisit",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_RecordedVisit_RecordedId",
                table: "Reviews",
                column: "RecordedId",
                principalTable: "RecordedVisit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecordedVisit_Clients_ClientId",
                table: "RecordedVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_RecordedVisit_RecordedId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecordedVisit",
                table: "RecordedVisit");

            migrationBuilder.RenameTable(
                name: "RecordedVisit",
                newName: "TempUsers");

            migrationBuilder.RenameIndex(
                name: "IX_RecordedVisit_ClientId",
                table: "TempUsers",
                newName: "IX_TempUsers_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempUsers",
                table: "TempUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_TempUsers_RecordedId",
                table: "Reviews",
                column: "RecordedId",
                principalTable: "TempUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TempUsers_Clients_ClientId",
                table: "TempUsers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
