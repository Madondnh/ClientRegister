using AutoMapper;
using Domain.DTOs.ClientDetailsDtos;
using Domain.Models;

namespace Application.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<ClientDetails, CreateClientDto>().ReverseMap();
    }
  }
}
