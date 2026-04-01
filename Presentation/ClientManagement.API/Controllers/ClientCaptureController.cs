using Application.Interfaces;
using Domain.DTOs.ClientDetailsDtos;
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
    /// <response code="201">Returns the newly created Client.</response>
    [HttpPost]
    [ProducesResponseType( typeof( ClientDetails ), StatusCodes.Status201Created )]
    public async Task<IActionResult> Create( [FromBody] CreateClientDto Client )
    {
      try
      {
        var addedClient = await _clientCaptureService.CreateClient( Client );

        return CreatedAtAction( nameof( Create ), new
        {
          id = addedClient.Id
        }, addedClient );
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
        // Log the exception here in a real Cliention app
        return StatusCode( 500, new
        {
          Message = "An error occurred while retrieving clients.",
          Details = ex.Message
        } );
      }
    }

    /// <summary>
    /// Updates an existing Client.
    /// </summary>
    /// <param name="id">The ID of the Client to update.</param>
    /// <param name="ClientName">The updated Client object.</param>
    /// <response code="204">Update successful.</response>
    /// <response code="400">If the ID in the URL does not match the ID in the body.</response>
    [HttpPut( "{id}" )]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    [ProducesResponseType( StatusCodes.Status400BadRequest )]
    public async Task<IActionResult> Update( string id, [FromBody] ClientDetails Client )
    {
      if(id != Client.Id)
        return BadRequest();

      await _clientCaptureService.UpdateClientAsync( Client );
      return NoContent();
    }

    /// <summary>
    /// Deletes a Client from the system.
    /// </summary>
    /// <param name="id">The ID of the Client to remove.</param>
    /// <response code="204">ClientDto deleted successfully.</response>
    [HttpDelete( "{id}" )]
    [ProducesResponseType( StatusCodes.Status204NoContent )]
    public async Task<IActionResult> Delete( string id )
    {
      await _clientCaptureService.DeleteClientAsync( id );
      return NoContent();
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