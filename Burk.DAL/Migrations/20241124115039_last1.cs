using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class last1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaitingLists_Clients_ClientId",
                table: "WaitingLists");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "WaitingLists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingLists_Clients_ClientId",
                table: "WaitingLists",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaitingLists_Clients_ClientId",
                table: "WaitingLists");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "WaitingLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingLists_Clients_ClientId",
                table: "WaitingLists",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
