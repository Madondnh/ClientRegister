using DisplayClientDetails.Web.Models;
using DisplayClientDetails.Web.Settings;
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
      var url = $"{_apiSettings.BaseUrl.TrimEnd('/')}/{AnalyticsEndpoints.Metrics}";
      var response = await _httpClient.GetAsync( url );
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