using Application.Interfaces;
using Domain.DTOs.ClientAnalyticsDtos;
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

  public async Task<IEnumerable<ClientDetailsAnalytics>> GetClientAnalyticsAsync()
  {
    return await _clientDetailsAnalyticsRepository.GetAllAsync();
  }

  public async Task<AnalyticsMetricsDto> GetRegistrationMetricsAsync()
  {
    var analytics = await GetClientAnalyticsAsync();

    return new AnalyticsMetricsDto
    {
      TotalUsers = analytics.Sum( a => a.UserCount ),

      UsersPerLocation = analytics
        .GroupBy( a => a.Location )
        .Select( g => (Location: g.Key, UserCount: g.Sum( a => a.UserCount )) )
        .ToList(),

      ClientsPerDate = analytics
        .GroupBy( a => a.RegistrationDate )
        .Select( g => (RegistrationDate: g.Key, ClientCount: g.Sum( a => a.ClientCount )) )
        .ToList()
    };
  }
}