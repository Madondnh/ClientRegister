using CapturingDetails.Web.Pages;
using Domain.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CapturingDetails.Web.Services;

public class ClientService : IClientService
{
  private const string ApiClientsEndpoint = "api/ClientCapture/Clients";
  private const string ApiClientsById = "api/ClientCapture/{0}";

  private readonly HttpClient _httpClient;
  private readonly IConfiguration _configuration;
  private string _apiBaseUrl = string.Empty;

  public ClientService( HttpClient httpClient, IConfiguration configuration )
  {
    _httpClient = httpClient;
    _configuration = configuration;
    _apiBaseUrl = configuration[ "ApiSettings:BaseUrl" ] ?? string.Empty;

    if(string.IsNullOrEmpty( _apiBaseUrl ))
    {
      throw new InvalidOperationException( "API Base URL is not configured in appsettings.json" );
    }

    // Set the base address if it's not already set
    if(_httpClient.BaseAddress == null)
    {
      _httpClient.BaseAddress = new Uri( _apiBaseUrl );
    }
  }

  /// <summary>
  /// Gets all clients from the API
  /// </summary>
  public async Task<List<ClientDetails>> GetAllClientsAsync()
  {
    try
    {
      var urlPath = $"{_apiBaseUrl}{ApiClientsEndpoint}";
      var response = await _httpClient.GetFromJsonAsync<List<ClientDetails>>( urlPath );
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
      var response = await _httpClient.GetFromJsonAsync<ClientDetails>( string.Format( ApiClientsById, id ) );
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
  public async Task<ClientDetails> CreateClientAsync( ClientDetails client )
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

      var response = await _httpClient.PostAsJsonAsync( ApiClientsEndpoint, client );
      response.EnsureSuccessStatusCode();

      var createdClient = await response.Content.ReadFromJsonAsync<ClientDetails>();
      ;
      return createdClient ?? throw new Exception( "Failed to create client: Empty response" );
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
  public async Task<ClientDetails> UpdateClientAsync( string id, ClientDetails client )
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

      var response = await _httpClient.PutAsJsonAsync( string.Format( ApiClientsById, id ), client );
      response.EnsureSuccessStatusCode();

      var updatedClient = await response.Content.ReadFromJsonAsync<ClientDetails>();
      return updatedClient ?? throw new Exception( "Failed to update client: Empty response" );
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

      var response = await _httpClient.DeleteAsync( string.Format( ApiClientsById, id ) );
      response.EnsureSuccessStatusCode();
    }
    catch(HttpRequestException ex)
    {
      throw new Exception( $"Failed to delete client: {ex.Message}", ex );
    }
  }
}
