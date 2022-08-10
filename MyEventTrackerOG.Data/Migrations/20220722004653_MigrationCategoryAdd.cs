using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEventTrackerOG.Data.Migrations
{
    public partial class MigrationCategoryAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MyEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyEvents_CategoryId",
                table: "MyEvents",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyEvents_Categories_CategoryId",
                table: "MyEvents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEvents_Categories_CategoryId",
                table: "MyEvents");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_MyEvents_CategoryId",
                table: "MyEvents");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MyEvents");
        }
    }
}
