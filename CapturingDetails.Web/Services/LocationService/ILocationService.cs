using Domain.Models;

namespace CapturingDetails.Web.Services;

public interface ILocationService
{
    /// <summary>
    /// Gets all Locations from the API
    /// </summary>
    Task<List<Location>> GetAllLocationsAsync();
}
