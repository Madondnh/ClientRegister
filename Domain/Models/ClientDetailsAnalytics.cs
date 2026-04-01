namespace Domain.Models
{
  public class ClientDetailsAnalytics 
  {
    public string Location { get; set; } = string.Empty;
    public int ClientCount
    {
      get; set;
    }
    public int UserCount
    {
      get; set;
    }
    public string RegistrationDate { get; set; } = string.Empty;
  }
}
