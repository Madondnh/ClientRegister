using Domain.Models;

namespace Application.Interfaces
{
  public interface IClientAnalyticsService 
  {
    Task<IEnumerable<ClientDetailsAnalytics>> GetClientAnalytics();
  }
}