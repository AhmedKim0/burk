using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Burk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixlast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AceeptedUsers");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "WaitingLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "WaitingLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TableNumber",
                table: "WaitingLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TempUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    Visitors = table.Column<int>(type: "int", nullable: true),
                    ReservationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttendanceTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    area = table.Column<int>(type: "int", nullable: false),
                    Smoking = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempUsers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WaitingLists_ClientId",
                table: "WaitingLists",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TempUsers_ClientId",
                table: "TempUsers",
                column: "ClientId");

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

            migrationBuilder.DropTable(
                name: "TempUsers");

            migrationBuilder.DropIndex(
                name: "IX_WaitingLists_ClientId",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "WaitingLists");

            migrationBuilder.DropColumn(
                name: "TableNumber",
                table: "WaitingLists");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.CreateTable(
                name: "AceeptedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    WaitingListId = table.Column<int>(type: "int", nullable: false),
                    AttendanceTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Smoking = table.Column<bool>(type: "bit", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    Visitors = table.Column<int>(type: "int", nullable: true),
                    area = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AceeptedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AceeptedUsers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AceeptedUsers_WaitingLists_WaitingListId",
                        column: x => x.WaitingListId,
                        principalTable: "WaitingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AceeptedUsers_ClientId",
                table: "AceeptedUsers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AceeptedUsers_WaitingListId",
                table: "AceeptedUsers",
                column: "WaitingListId");
        }
    }
}
