using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaccination_Mgmt.Migrations
{
    public partial class DeleteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[DeleteUser]
                            @UserId int
                        AS
                        BEGIN
                            delete from UserRoles
	                        where UserId=@UserId;
	                        delete from DoseMappings
	                        where UserId=@UserId;
                            delete from Users
                            where Id=@UserId;
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
