using CapturingDetails.Web.Pages;
using DisplayClientDetails.Web.Settings;
using Domain.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CapturingDetails.Web.Services.ClientService;

public class ClientService : IClientService
{
  private readonly HttpClient _httpClient;

  public ClientService( HttpClient httpClient )
  {
    _httpClient = httpClient;
  }

  /// <summary>
  /// Gets all clients from the API
  /// </summary>
  public async Task<List<ClientDetails>> GetAllClientsAsync()
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<List<ClientDetails>>( ClientCaptureEndpoints.ApiClientsEndpoint );
      return response ?? new List<ClientDetails>();
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

  /// <summary>
  /// Gets a single client by ID
  /// </summary>
  public async Task<ClientDetails?> GetClientByIdAsync( string id )
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<ClientDetails>( string.Format( ClientCaptureEndpoints.ApiClientsById, id ) );
      return response;
    }
    catch(HttpRequestException ex)
    {
      throw new Exception( $"Failed to get client with ID {id} from API: {ex.Message}", ex );
    }
    catch(JsonException ex)
    {
      throw new Exception( $"Failed to deserialize API response: {ex.Message}", ex );
    }
  }

  /// <summary>
  /// Creates a new client
  /// </summary>
  public async Task<HttpResponseMessage > CreateClientAsync( ClientDetails client )
  {
    try
    {
      if(client == null)
      {
        throw new ArgumentNullException( nameof( client ) );
      }

      if(string.IsNullOrWhiteSpace( client.ClientName ))
      {
        throw new ArgumentException( "Client name is required", nameof( client.ClientName ) );
      }

      return await _httpClient.PostAsJsonAsync( ClientCaptureEndpoints.ApiClientsCreateEndpoint, client );
    }
    catch(HttpRequestException ex)
    {
      throw new Exception( $"Failed to create client: {ex.Message}", ex );
    }
    catch(JsonException ex)
    {
      throw new Exception( $"Failed to deserialize API response: {ex.Message}", ex );
    }
  }

  /// <summary>
  /// Updates an existing client
  /// </summary>
  public async Task<HttpResponseMessage> UpdateClientAsync( string id, ClientDetails client )
  {
    try
    {
      if(string.IsNullOrWhiteSpace( id ))
      {
        throw new ArgumentException( "Client ID is required", nameof( id ) );
      }

      if(client == null)
      {
        throw new ArgumentNullException( nameof( client ) );
      }

      if(string.IsNullOrWhiteSpace( client.ClientName ))
      {
        throw new ArgumentException( "Client name is required", nameof( client.ClientName ) );
      }

      return await _httpClient.PutAsJsonAsync( string.Format( ClientCaptureEndpoints.ApiClientsById, id ), client );
    }
    catch(HttpRequestException ex)
    {
      throw new Exception( $"Failed to update client: {ex.Message}", ex );
    }
    catch(JsonException ex)
    {
      throw new Exception( $"Failed to deserialize API response: {ex.Message}", ex );
    }
  }

  /// <summary>
  /// Deletes a client by ID
  /// </summary>
  public async Task DeleteClientAsync( string id )
  {
    try
    {
      if(string.IsNullOrWhiteSpace( id ))
      {
        throw new ArgumentException( "Client ID is required", nameof( id ) );
      }

      var response = await _httpClient.DeleteAsync( string.Format( ClientCaptureEndpoints.ApiClientsById, id ) );
      response.EnsureSuccessStatusCode();
    }
    catch(HttpRequestException ex)
    {
      throw new Exception( $"Failed to delete client: {ex.Message}", ex );
    }
  }
}
