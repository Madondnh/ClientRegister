using DisplayClientDetails.Web.Settings;
using Domain.DTOs.ClientAnalyticsDtos;
using Domain.Models;
using System.Net.Http.Json;

namespace DisplayClientDetails.Web.Services;

public class AnalyticsService
{
  private readonly HttpClient _httpClient;
  private readonly ApiSettings _apiSettings;

  public AnalyticsService( HttpClient httpClient, ApiSettings apiSettings )
  {
    _httpClient = httpClient;
    _apiSettings = apiSettings;
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