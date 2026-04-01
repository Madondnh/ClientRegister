using Domain.Models;

namespace Application.Interfaces
{
  public interface IClientCaptureService
  {
    Task<ClientDetails> CreateClient( ClientDetails clientDetails );
    Task<IEnumerable<ClientDetails>> GetClients( );
  }
}
