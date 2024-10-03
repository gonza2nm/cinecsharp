using AutoMapper;
using backend_cine.Dbcontext;
using backend_cine.DTOs;
using backend_cine.Interfaces;
using backend_cine.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_cine.Controllers;

public class UserController(IMapper mapper, DbContextCinema dbContext) : ControllerBase, IRepository<UserDTO, UserRequestDTO>
{
  private readonly IMapper _mapper = mapper;
  private readonly DbContextCinema _dbContext = dbContext;

  public Task<ActionResult<ResponseList<UserDTO>>> FindAll()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<UserDTO>>> FindOne(long id)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<UserDTO>>> Add(UserRequestDTO obj)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<UserDTO>>> Update(long id, UserRequestDTO obj)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<UserDTO>>> Delete(long id)
  {
    throw new NotImplementedException();
  }
}