/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_cine.Models;
using backend_cine.Dbcontext;
using backend_cine.Interfaces;
using backend_cine.Responses;
using Microsoft.VisualBasic;

namespace backend_cine.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/movies")]
public class MovieController : ControllerBase, ICrud<Movie>
{
  private readonly DbContext _context;

  public MovieController(DbContext dbContext)
  {
    _context = dbContext;
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public Task<ActionResult<ResponseList<Movie>>> FindAll()
  {
    throw new NotImplementedException();
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public Task<ActionResult<ResponseOne<Movie>>> FindOne(long id)
  {
    throw new NotImplementedException();
  }

  [HttpPost]
  [Consumes("application/json")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public Task<ActionResult<ResponseOne<Movie>>> Add(Movie obj)
  {
    throw new NotImplementedException();
  }

  [HttpPut("{id}")]
  [Consumes("application/json")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public Task<ActionResult<ResponseOne<Movie>>> Update(long id, Movie obj)
  {
    throw new NotImplementedException();
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public Task<ActionResult<ResponseOne<Movie>>> Delete(long id)
  {
    throw new NotImplementedException();
  }


}*/
