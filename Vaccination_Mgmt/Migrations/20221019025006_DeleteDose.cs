using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaccination_Mgmt.Migrations
{
    public partial class DeleteDose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[DeleteDose]
                            @DoseId int
                        AS
                        BEGIN
                            delete from DoseMappings
	                        where DoseId=@DoseId;
	                        delete from Doses
	                        where id=@DoseId;
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
