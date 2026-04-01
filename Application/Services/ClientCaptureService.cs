using Application.Interfaces;
using AutoMapper;
using Domain.DTOs.ClientDetailsDtos;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ClientCaptureService : IClientCaptureService
{
  private readonly IRepository<ClientDetails>? _clientDetailsRepository;
  private readonly IMapper _mapper;
  public ClientCaptureService( IRepository<ClientDetails> clientDetailsRepository, IMapper mapper )
  {
    _clientDetailsRepository = clientDetailsRepository;
    _mapper = mapper;
  }

  public async Task<ClientDetails> CreateClient( CreateClientClientDto clientDetails )
  {
    var client = _mapper.Map<ClientDetails>( clientDetails );
    return await _clientDetailsRepository.InsertAsync( client );
  }

  public async Task<IEnumerable<ClientDetails>> GetClients()
  {
    return await _clientDetailsRepository.GetAllAsync( );
  }

  public async Task<ClientDetails> UpdateClientAsync( ClientDetails clientDetails )
  {
    return await _clientDetailsRepository.UpdateAsync( clientDetails );
  }
  public async Task DeleteClientAsync( string id )
  {
    await _clientDetailsRepository.DeleteById( id );
  }
}