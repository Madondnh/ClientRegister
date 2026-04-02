namespace DisplayClientDetails.Web.Settings;

public class ApiSettings
{
    public string BaseClientManagerUrl { get; set; } = string.Empty;
}

public class AnalyticsEndpoints
{
    public const string Metrics = "api/ClientAnalytics/RegistrationMetrics";
}
public class Date
{
  public const string DateDisplayFormat = "dd MMM yyyy";
}