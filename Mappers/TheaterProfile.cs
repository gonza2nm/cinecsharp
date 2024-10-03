using AutoMapper;
using backend_cine.DTOs;
using backend_cine.Models;

namespace backend_cine.Mapper;

public class TheaterProfile : Profile
{
  public TheaterProfile()
  {
    CreateMap<Theater, TheaterDTO>().ReverseMap();
    CreateMap<TheaterDTO, TheaterRequestDTO>().ReverseMap();
    CreateMap<Theater, TheaterRequestDTO>().ReverseMap();
  }
}