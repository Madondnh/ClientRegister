using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClientAnalyticsViewnitialCreat : Migration
    {
    /// <inheritdoc />
    protected override void Up( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.Sql( @"
            CREATE VIEW View_ClientAnalytics AS
            SELECT 
                Location,
                COUNT(Id) AS ClientCount,
                SUM(NumberOfUsers) AS UserCount,
                DATE(DateRegistered) AS RegistrationDate
            FROM ClientDetails
            GROUP BY Location, DATE(DateRegistered);
        " );
    }

    protected override void Down( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.Sql( "DROP VIEW View_ClientAnalytics;" );
    }
  }
}
