using AutoMapper;
using backend_cine.DTOs;
using backend_cine.Models;

namespace backend_cine.mapping;

public class MovieProfile : Profile
{
  public MovieProfile()
  {
    CreateMap<Movie, MovieDTO>().ReverseMap();
  }
}