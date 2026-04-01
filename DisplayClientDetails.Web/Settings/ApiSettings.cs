namespace DisplayClientDetails.Web.Settings;

public class ApiSettings
{
    public string BaseClientManagerUrl { get; set; } = string.Empty;
}

public class AnalyticsEndpoints
{
    public const string Metrics = "api/ClientAnalytics/RegistrationMetrics";
}