using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]
  [Produces( "application/json" )] // Tells Swagger all endpoints return JSON
  public class LocationController : ControllerBase
  {
    private readonly ILocationService _locationService;

    public LocationController( ILocationService locationService )
    {
      _locationService = locationService;
    }

    /// <summary>
    /// returns a list of Registered countries in the world.
    /// </summary>

    [HttpGet( "Locations" )]
    [HttpDelete( "{id}" )]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> GetLocations()
    {
      try
      {
        var locations = await _locationService.GetLocationsAsync();
        return Ok( locations );
      }
      catch(Exception ex)
      {
        // Log the exception here in a real Cliention app
        return StatusCode( 500, new
        {
          Message = "An error occurred while retrieving Locations.",
          Details = ex.Message
        } );
      }
    }
  }
}