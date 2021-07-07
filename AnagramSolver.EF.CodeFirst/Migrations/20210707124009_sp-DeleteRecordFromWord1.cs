using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class spDeleteRecordFromWord1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE PROCEDURE [dbo].[DeleteRecordFromWord]
	                @target nvarchar(50)
                AS
                BEGIN
	                SET NOCOUNT ON;
	                DELETE FROM Word WHERE Word=@target
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
