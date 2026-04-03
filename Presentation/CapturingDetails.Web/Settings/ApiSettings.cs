namespace CapturingDetails.Web.Settings;

public class ApiSettings
{
    public string BaseClientManagerUrl { get; set; } = string.Empty;
}

public class ClientCaptureEndpoints
{
  public const string ApiClientsEndpoint = "api/ClientCapture/Clients";
  public const string ApiClientsCreateEndpoint = "api/ClientCapture";
  public const string ApiClientsById = "api/ClientCapture/{0}";
}
public class LocationEndpoints
{
  public const string ApiAllLocationsEndpoint = "api/Location/Locations";
}

public class Date
{
  public const string DateDisplayFormat = "dd MMM yyyy";
}