using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class LocationService : ILocationService
{
  private readonly IRepository<Location>? _locationRepository;

  public LocationService( IRepository<Location> locationRepository )
  {
    _locationRepository = locationRepository;
  }

  public async Task<IEnumerable<Location>> GetLocationsAsync()
  {
    return await _locationRepository.GetAllAsync(); 
  }
}