using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class spInsertUserLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp;

            sp = @"CREATE PROCEDURE [dbo].[InsertUserLog]
	                    @userIp nvarchar(50),
	                    @searchingWord nvarchar(50),
	                    @searchTime datetime,
	                    @foundAnagramId int
                AS
                BEGIN
	                SET NOCOUNT ON;
	                INSERT INTO UserLog(Ip, SearchingWord, SearchTime, WordID)
		                VALUES(@userIp, @searchingWord, @searchTime, @foundAnagramId)
                END";

            migrationBuilder.Sql(sp);

            sp = @"CREATE PROCEDURE [dbo].[WordInsert]
	                @Word varchar(50),
	                @Category varchar(50)
                AS
                BEGIN
	                INSERT INTO Word (Word, Category) VALUES (@Word, @Category)
                END";

            migrationBuilder.Sql(sp);

            sp = @"CREATE PROCEDURE [dbo].[GetUserLog]
                AS
                BEGIN
	                SET NOCOUNT ON;
	                SELECT UserLogID, Ip, SearchingWord, SearchTime, 
		                (SELECT Word FROM Word WHERE WordID = ID) AS Anagram
		                from UserLog
                END";

            migrationBuilder.Sql(sp);         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
