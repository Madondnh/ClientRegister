using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]
  [Produces( "application/json" )] // Tells Swagger all endpoints return JSON
  public class ClientCaptureController : ControllerBase
  {
    private readonly IClientAnalyticsService _service;
  }
}