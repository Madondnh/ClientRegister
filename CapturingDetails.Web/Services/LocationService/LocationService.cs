using DisplayClientDetails.Web.Settings;
using Domain.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CapturingDetails.Web.Services.ClientService;

public class LocationService : ILocationService
{
  private readonly HttpClient _httpClient;
  public LocationService( HttpClient httpClient )
  {
    _httpClient = httpClient;
  }

  public async Task<List<Location>> GetAllLocationsAsync()
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<List<Location>>( LocationEndpoints.ApiAllLocationsEndpoint );
      return response ?? new List<Location>();
    }
    catch(HttpRequestException ex)
    {
      throw new Exception( $"Failed to get clients from API: {ex.Message}", ex );
    }
    catch(JsonException ex)
    {
      throw new Exception( $"Failed to deserialize API response: {ex.Message}", ex );
    }
  }
}
