using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEventTrackerOG.Data.Migrations
{
    public partial class YetAnotherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "MyEvents");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "MyEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyEvents_LocationId",
                table: "MyEvents",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyEvents_Locations_LocationId",
                table: "MyEvents",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEvents_Locations_LocationId",
                table: "MyEvents");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_MyEvents_LocationId",
                table: "MyEvents");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MyEvents");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "MyEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
