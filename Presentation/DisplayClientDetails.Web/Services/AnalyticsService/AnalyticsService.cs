using DisplayClientDetails.Web.Settings;
using Domain.DTOs.ClientAnalyticsDtos;
using System.Net.Http.Json;

namespace DisplayClientDetails.Web.Services.AnalyticsService.AnalyticsService;

public class AnalyticsService : IAnalyticsService
{
  private readonly HttpClient _httpClient;

  public AnalyticsService( HttpClient httpClient )
  {
    _httpClient = httpClient;
  }

  public async Task<AnalyticsMetricsDto?> GetAnalyticsMetricsAsync()
  {
    try
    {
      var response = await _httpClient.GetAsync( AnalyticsEndpoints.Metrics );
      if(response.IsSuccessStatusCode)
      {
        return await response.Content.ReadFromJsonAsync<AnalyticsMetricsDto>();
      }
      return null;
    }
    catch(Exception ex)
    {
      Console.WriteLine( $"Error fetching analytics metrics: {ex.Message}" );
      return null;
    }
  }
}