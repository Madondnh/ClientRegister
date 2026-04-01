using Application.Interfaces;
using Application.Services;
using Domain.Models;
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

    /// <summary>
    /// Creates a new client in the client registry .
    /// </summary>
    /// <response code="201">Returns the newly created product.</response>
    [HttpPost]
    [ProducesResponseType( typeof( ClientDetails ), StatusCodes.Status201Created )]
    public async Task<IActionResult> Create( [FromBody] ClientDetails product )
    {
      try
      {
        var addedClient = await _clientCaptureService.CreateClient( product );

        return CreatedAtAction( nameof( Create ), new
        {
          id = addedClient.Id
        }, addedClient );
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

    /// <summary>
    /// Returns a list of all the registered clients .
    /// </summary>
    /// 
    [HttpGet( "Clients" )]
    public async Task<IActionResult> GetClients()
    {
      try
      {
        var clients = await _clientCaptureService.GetClients();
        return Ok( clients );
      }
      catch(Exception ex)
      {
        // Log the exception here in a real production app
        return StatusCode( 500, new
        {
          Message = "An error occurred while retrieving clients.",
          Details = ex.Message
        } );
      }
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