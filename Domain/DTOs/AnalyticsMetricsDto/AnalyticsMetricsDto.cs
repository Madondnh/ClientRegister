namespace Domain.DTOs.ClientAnalyticsDtos
{
  public class AnalyticsMetricsDto
  {
    public int TotalUsers { get; set; }
    public List<(string Location, int UserCount)> UsersPerLocation { get; set; }
    public List<(string RegistrationDate, int ClientCount)> ClientsPerDate { get; set; }
  }
}
