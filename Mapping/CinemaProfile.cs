using AutoMapper;
using backend_cine.DTOs;
using backend_cine.Models;

namespace backend_cine.mapping;

public class CinemaProfile : Profile
{
  public CinemaProfile()
  {
    CreateMap<Cinema, CinemaDTO>().ReverseMap();
  }
}