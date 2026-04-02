namespace Domain.DTOs
{
  public class AnalyticsMetricsDto
  {
    public int TotalUsers { get; set; }
    public List<UsersPerLocationDto> UsersPerLocation { get; set; }
    public List<ClientsPerDateDto> ClientsPerDate { get; set; }
  }
}
