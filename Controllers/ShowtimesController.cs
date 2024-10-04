using AutoMapper;
using backend_cine.Dbcontext;
using backend_cine.DTOs;
using backend_cine.Interfaces;
using backend_cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Controllers;

[ApiController]
[Route("api/showtimes")]
public class ShowtimesController(DbContextCinema dbContext, IMapper mapper) : ControllerBase, IRepository<ShowtimeDTO, ShowtimeRequestDTO>
{
  private readonly IMapper _mapper = mapper;
  private readonly DbContextCinema _dbContext = dbContext;

  [HttpGet]
  public Task<ActionResult<ResponseList<ShowtimeDTO>>> FindAll()
  {
    throw new NotImplementedException();
  }
  [HttpGet("{id}")]
  public Task<ActionResult<ResponseOne<ShowtimeDTO>>> FindOne(long id)
  {
    throw new NotImplementedException();
  }

  [HttpPost]
  public async Task<ActionResult<ResponseOne<ShowtimeDTO>>> Add(ShowtimeRequestDTO showtimeBody)
  {
    var res = new ResponseOne<ShowtimeDTO> { Status = "", Message = "", Data = null, Error = null };
    if (!ModelState.IsValid)
    {
      res.UpdateValues("400", "Incorrect or Invalid data", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      var movie = await _dbContext.Movies.Include(m => m.Languages).Include(m => m.Formats).FirstOrDefaultAsync(m => m.Id == showtimeBody.MovieId);
      var theater = await _dbContext.Theaters.FirstOrDefaultAsync(t => t.Id == showtimeBody.TheaterId);
      if (movie is null || theater is null)
      {
        var message = movie is null ? "Movie not found" : "Theater not found";
        res.UpdateValues("404", message, null, null);
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var language = await _dbContext.Languages.FirstOrDefaultAsync(l => l.Id == showtimeBody.LanguageId);
      var format = await _dbContext.Formats.FirstOrDefaultAsync(f => f.Id == showtimeBody.FormatId);
      if (language is null || format is null)
      {
        var message = language is null ? "Language not found" : "Format not found";
        res.UpdateValues("404", message, null, null);
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      if (movie.Formats.Any(f => f.Id == format.Id) && movie.Languages.Any(l => l.Id == language.Id))
      {


        //hacer una funcion para que las funciones no se solapen segun la sala y la hora
        Showtime showtimeDB = new Showtime
        {
          StartDate = showtimeBody.StartDate,
          FinishDate = showtimeBody.StartDate.AddMinutes(movie.Duration),
          MovieId = movie.Id,
          LanguageId = language.Id,
          FormatId = format.Id,
          TheaterId = theater.Id,
          Movie = movie,
          Language = language,
          Format = format,
          Theater = theater
        };
        await _dbContext.Showtimes.AddAsync(showtimeDB);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        var showtimeDTO = _mapper.Map<ShowtimeDTO>(showtimeDB);
        res.UpdateValues("201", "Showtime created successfully", showtimeDTO, null);
        return StatusCode(StatusCodes.Status201Created, res);
      }
      else
      {
        res.UpdateValues("400", "the format or language of the feature does not exist in the movie", null, "Bad Request");
        return StatusCode(StatusCodes.Status400BadRequest, res);
      }
    }
    catch (DbUpdateException dbEx)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "Database error occurred.", null, dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

  [HttpPut("{id}")]
  public Task<ActionResult<ResponseOne<ShowtimeDTO>>> Update(long id, ShowtimeRequestDTO showtimeBody)
  {
    throw new NotImplementedException();
  }

  [HttpDelete("{id}")]
  public Task<ActionResult<ResponseOne<ShowtimeDTO>>> Delete(long id)
  {
    throw new NotImplementedException();
  }
}