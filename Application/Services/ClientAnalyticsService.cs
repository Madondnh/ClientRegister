using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ClientAnalyticsService : IClientAnalyticsService
{
  private readonly IViewsRepository<ClientDetailsAnalytics>? _clientDetailsAnalyticsRepository;

  public ClientAnalyticsService( IViewsRepository<ClientDetailsAnalytics> clientDetailsAnalyticsRepository )
  {
    _clientDetailsAnalyticsRepository = clientDetailsAnalyticsRepository;
  }

  public async Task<IEnumerable<ClientDetailsAnalytics>> GetClientAnalytics()
  {
    return await _clientDetailsAnalyticsRepository.GetAllAsync();
  }
}