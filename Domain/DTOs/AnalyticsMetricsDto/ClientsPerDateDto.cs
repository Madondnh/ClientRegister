namespace Domain.DTOs
{
  public class ClientsPerDateDto
  {
    public DateOnly RegistrationDate
    {
      get; set;
    }
    public int ClientCount
    {
      get; set;
    }
 
  }
}
