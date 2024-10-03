using AutoMapper;
using backend_cine.DTOs;
using backend_cine.Models;

namespace backend_cine;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<User, UserDTO>().ReverseMap();
    CreateMap<User, UserRequestDTO>().ReverseMap();
    CreateMap<UserDTO, UserRequestDTO>().ReverseMap();
  }
}