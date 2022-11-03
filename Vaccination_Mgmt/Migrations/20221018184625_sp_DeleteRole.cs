using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaccination_Mgmt.Migrations
{
    public partial class sp_DeleteRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[DeleteRole]
                            @RoleId int
                        AS
                        BEGIN
                            delete from UserRoles
	                        where RoleId=@RoleId;
	                        delete from Roles
	                        where id=@RoleId;
                        END";
            migrationBuilder.Sql(sp);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
