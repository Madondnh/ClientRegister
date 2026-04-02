using Domain.DTOs;
using Domain.Models;

namespace Application.Interfaces
{
  public interface IClientAnalyticsService 
  {
    Task<IEnumerable<ClientDetailsAnalytics>> GetClientAnalyticsAsync();
    Task<AnalyticsMetricsDto> GetRegistrationMetricsAsync();
  }
}