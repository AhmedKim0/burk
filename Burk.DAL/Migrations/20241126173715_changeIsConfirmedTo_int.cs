using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeIsConfirmedTo_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsConfirmed",
                table: "WaitingLists",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsConfirmed",
                table: "WaitingLists",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
