namespace DisplayClientDetails.Web.Models;

public class AnalyticsMetricsDto
{
    public int TotalUsers { get; set; }
    public List<(string Location, int UserCount)> UsersPerLocation { get; set; } = new();
    public List<(string RegistrationDate, int ClientCount)> ClientsPerDate { get; set; } = new();
}