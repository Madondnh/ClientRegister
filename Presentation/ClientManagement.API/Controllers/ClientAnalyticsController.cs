using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
  namespace Catalog.API.Controllers
  {
    [ApiController]
    [Route( "api/[controller]" )]
    [Produces( "application/json" )]
    public class ClientAnalyticsController : ControllerBase
    {
      private readonly IClientAnalyticsService _clientAnalyticsService;

      public ClientAnalyticsController( IClientAnalyticsService clientAnalyticsService )
      {
        _clientAnalyticsService = clientAnalyticsService;
      }

      [HttpGet( "metrics" )]
      public async Task<IActionResult> GetClientMetrics()
      {
        try
        {
          // The service should return a DTO containing the three metrics
          var metrics = await _clientAnalyticsService.GetClientAnalyticsAsync();

          if(metrics == null)
          {
            return NotFound( new
            {
              Message = "No data available to calculate metrics."
            } );
          }

          return Ok( metrics );
        }
        catch(Exception ex)
        {
          // Log the exception here in a real production app
          return StatusCode( 500, new
          {
            Message = "An error occurred while retrieving analytics.",
            Details = ex.Message
          } );
        }
      }

      /// <summary>
      /// Retrieves a summary of registration metrics including user totals and geographical distribution.
      /// </summary>
      /// <returns>A DTO containing user counts, location-based stats, and daily registration trends.</returns>
      [HttpGet( "RegistrationMetrics" )]
      [ProducesResponseType( StatusCodes.Status200OK, Type = typeof( AnalyticsMetricsDto ) )]
      [ProducesResponseType( StatusCodes.Status500InternalServerError )]
      public async Task<ActionResult<AnalyticsMetricsDto>> GetRegistrationMetrics()
      {
        try
        {
          var metrics = await _clientAnalyticsService.GetRegistrationMetricsAsync();
          return Ok( metrics );
        }
        catch(Exception ex)
        {
          // _logger.LogError(ex, "Failed to fetch metrics");
          return StatusCode( 500, "Internal server error while fetching metrics" );
        }
      }
    }
  }
}
