using Domain.DTOs.ClientDetailsDtos;
using Domain.Models;

namespace Application.Interfaces
{
  public interface IClientCaptureService
  {
    Task<ClientDetails> CreateClient( CreateClientDto clientDetails );
    Task<ClientDetails> UpdateClientAsync( ClientDetails clientDetails );

    Task DeleteClientAsync( string id );
    
    Task<IEnumerable<ClientDetails>> GetClients( );
  }
}
