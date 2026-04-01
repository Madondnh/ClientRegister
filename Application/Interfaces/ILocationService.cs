using Domain.Models;

namespace Application.Interfaces
{
  public interface ILocationService   
  {
    Task<IEnumerable<Location>> GetLocationsAsync();
  }
}
