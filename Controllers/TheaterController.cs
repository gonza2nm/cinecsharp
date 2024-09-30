using backend_cine.DTOs;
using backend_cine.Interfaces;
using backend_cine.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_cine.Controllers;

public class TheaterController : ControllerBase, IRepository<TheaterDTO, TheaterRequestDTO>
{
  public Task<ActionResult<ResponseOne<TheaterDTO>>> Add(TheaterRequestDTO obj)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<TheaterDTO>>> Delete(long id)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseList<TheaterDTO>>> FindAll()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<TheaterDTO>>> FindOne(long id)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<TheaterDTO>>> Update(long id, TheaterRequestDTO obj)
  {
    throw new NotImplementedException();
  }
}