using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ClientCaptureService : IClientCaptureService
{
  private readonly IRepository<ClientDetails>? _clientDetailsRepository;

  private readonly IMapper _mapper;

  public void Dispose()
  {
    throw new NotImplementedException();
  }
}