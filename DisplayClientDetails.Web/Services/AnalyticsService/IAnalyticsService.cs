using Domain.DTOs.ClientAnalyticsDtos;

namespace DisplayClientDetails.Web.Services.AnalyticsService;

public interface IAnalyticsService
{
  public Task<AnalyticsMetricsDto?> GetAnalyticsMetricsAsync();
}