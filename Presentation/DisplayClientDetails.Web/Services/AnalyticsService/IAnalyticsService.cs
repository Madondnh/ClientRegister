using Domain.DTOs;

namespace DisplayClientDetails.Web.Services.AnalyticsService;

public interface IAnalyticsService
{
  public Task<AnalyticsMetricsDto?> GetAnalyticsMetricsAsync();
}