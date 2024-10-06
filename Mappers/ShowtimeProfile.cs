using AutoMapper;
using backend_cine.DTOs;
using backend_cine.Models;

namespace backend_cine.Mapper;

public class ShowtimeProfile : Profile
{
  public ShowtimeProfile()
  {
    CreateMap<Showtime, ShowtimeDTO>().ReverseMap();
    CreateMap<Showtime, ShowtimeRequestDTO>().ReverseMap();
    CreateMap<ShowtimeDTO, ShowtimeRequestDTO>().ReverseMap();
  }
}