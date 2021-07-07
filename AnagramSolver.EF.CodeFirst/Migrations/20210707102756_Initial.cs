using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CachedWord",
                columns: table => new
                {
                    CachedWordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchingWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WordID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedWord", x => x.CachedWordID);
                    table.ForeignKey(
                        name: "FK_CachedWord_Word_WordID",
                        column: x => x.WordID,
                        principalTable: "Word",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLog",
                columns: table => new
                {
                    UserLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearcingWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SearchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WordID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLog", x => x.UserLogID);
                    table.ForeignKey(
                        name: "FK_UserLog_Word_WordID",
                        column: x => x.WordID,
                        principalTable: "Word",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedWord_WordID",
                table: "CachedWord",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_UserLog_WordID",
                table: "UserLog",
                column: "WordID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedWord");

            migrationBuilder.DropTable(
                name: "UserLog");

            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
