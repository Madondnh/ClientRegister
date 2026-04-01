using Application.Interfaces;
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
          var metrics = await _clientAnalyticsService.GetClientAnalytics();

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
    }
  }
}
