using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_cine.Models;
using backend_cine.Dbcontext;
using backend_cine.Interfaces;
using backend_cine.Responses;

namespace backend_cine.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/movies")]
public class MovieController : ControllerBase, ICrud<Movie>
{
  public Task<ActionResult<ResponseOne<Movie>>> Add(Movie obj)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<Movie>>> Delete(long id)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseList<Movie>>> FindAll()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<Movie>>> FindOne(long id)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<ResponseOne<Movie>>> Update(long id, Movie obj)
  {
    throw new NotImplementedException();
  }
}
