using AutoMapper;
using backend_cine.DTOs;
using backend_cine.Models;

namespace backend_cine.Mapper;

public class CinemaProfile : Profile
{
  public CinemaProfile()
  {
    CreateMap<Cinema, CinemaDTO>().ReverseMap();
    CreateMap<Cinema, CinemaRequestDTO>().ReverseMap();
    CreateMap<CinemaDTO, CinemaRequestDTO>().ReverseMap();

  }
}