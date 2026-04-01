namespace DisplayClientDetails.Web.Settings;

public class ApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;
}

public class AnalyticsEndpoints
{
    public const string Metrics = "api/ClientAnalytics/RegistrationMetrics";
}