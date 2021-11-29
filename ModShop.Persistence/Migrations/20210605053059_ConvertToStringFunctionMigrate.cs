using Microsoft.EntityFrameworkCore.Migrations;

namespace ModShop.Persistence.Migrations
{
    public partial class ConvertToStringFunctionMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION fn_ConvertToString(@input int)
                RETURNS nvarchar(4000)
                BEGIN
                RETURN CONVERT(varchar(16),@input)
                END
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP FUNCTION fn_ConvertToString
            ");
        }
    }
}
