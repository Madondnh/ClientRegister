using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]
  [Produces( "application/json" )] // Tells Swagger all endpoints return JSON
  public class ClientCaptureController : ControllerBase
  {
    private readonly IClientCaptureService _clientCaptureService;
    private readonly ILocationService _locationService;


    public ClientCaptureController( IClientCaptureService clientCaptureService, ILocationService locationService )
    {
      _clientCaptureService = clientCaptureService;
      _locationService = locationService;
    }

    [HttpGet( "Locations" )]
    public async Task<IActionResult> GetLocations()
    {
      try
      {
        var locations = await _locationService.GetLocationsAsync();
        return Ok( locations );
      }
      catch(Exception ex)
      {
        // Log the exception here in a real production app
        return StatusCode( 500, new
        {
          Message = "An error occurred while retrieving Locations.",
          Details = ex.Message
        } );
      }
    }
  }
}