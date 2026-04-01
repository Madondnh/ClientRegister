using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ClientCaptureService : IClientCaptureService
{
  private readonly IRepository<ClientDetails>? _clientDetailsRepository;

  public ClientCaptureService( IRepository<ClientDetails> clientDetailsRepository )
  {
    _clientDetailsRepository = clientDetailsRepository;
  }

  public async Task<ClientDetails> CreateClient( ClientDetails clientDetails )
  {
    return await _clientDetailsRepository.InsertAsync( clientDetails );
  }

  public async Task<IEnumerable<ClientDetails>> GetClients()
  {
    return await _clientDetailsRepository.GetAllAsync( );
  }
}