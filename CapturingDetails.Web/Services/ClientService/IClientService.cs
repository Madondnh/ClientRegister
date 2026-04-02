using Domain.Models;

namespace CapturingDetails.Web.Services.ClientService;

public interface IClientService
{
    /// <summary>
    /// Gets all clients from the API
    /// </summary>
    Task<List<ClientDetails>> GetAllClientsAsync();

    /// <summary>
    /// Gets a single client by ID
    /// </summary>
    Task<ClientDetails?> GetClientByIdAsync(string id);

    /// <summary>
    /// Creates a new client
    /// </summary>
    Task<HttpResponseMessage> CreateClientAsync(ClientDetails client);

    /// <summary>
    /// Updates an existing client
    /// </summary>
    Task<HttpResponseMessage> UpdateClientAsync(string id, ClientDetails client);

    /// <summary>
    /// Deletes a client by ID
    /// </summary>
    Task DeleteClientAsync(string id);
}
