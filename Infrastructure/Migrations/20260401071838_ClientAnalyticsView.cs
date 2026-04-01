using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class ClientAnalyticsView : Migration
  {
    protected override void Up( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.Sql( @"
CREATE VIEW View_ClientAnalytics AS
SELECT 
    l.LocationName AS Location,
    COUNT(c.Id) AS ClientCount,
    SUM(c.NumberOfUsers) AS UserCount,
    DATE(c.DateRegistered) AS RegistrationDate
FROM ClientDetails c
JOIN Location l ON c.LocationId = l.Id
GROUP BY l.LocationName, DATE(c.DateRegistered);"
);
    }

    protected override void Down( MigrationBuilder migrationBuilder )
    {
      migrationBuilder.Sql( "DROP VIEW View_ClientAnalytics;" );
    }
  }
}
