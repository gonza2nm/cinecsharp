using System.Data.Common;
using AutoMapper;
using backend_cine.Dbcontext;
using backend_cine.DTOs;
using backend_cine.Interfaces;
using backend_cine.Models;
using backend_cine.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_cine.Controllers;

[ApiController]
[Route("api/showtimes")]
public class ShowtimesController(DbContextCinema dbContext, IMapper mapper, ShowtimeService showtimeService) : ControllerBase, IRepository<ShowtimeDTO, ShowtimeRequestDTO>
{
  private readonly IMapper _mapper = mapper;
  private readonly DbContextCinema _dbContext = dbContext;
  private readonly ShowtimeService _service = showtimeService;

  [HttpGet]
  public async Task<ActionResult<ResponseList<ShowtimeDTO>>> FindAll()
  {
    var res = new ResponseList<ShowtimeDTO>() { Status = "", Message = "", Data = [], Error = null };
    try
    {
      var showtimesDB = await _service.GetShowtimesAsync();
      var showtimes = _mapper.Map<List<ShowtimeDTO>>(showtimesDB);
      res.UpdateValues("200", "Found showtimes", showtimes, null);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (Exception ex)
    {
      res.UpdateValues("500", "An error occurred while processing your request.", [], ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
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
      var movie = await _service.SearchMovieAsync(showtimeBody.MovieId);
      var theater = await _service.SearchTheaterAsync(showtimeBody.TheaterId);
      if (movie is null || theater is null)
      {
        var message = movie is null ? "Movie not found" : "Theater not found";
        res.UpdateValues("404", message, null, null);
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var cinemaContainMovie = await _service.CinemaContainMovieAsync(theater.Id, movie.Id);
      if (!cinemaContainMovie)
      {
        await transaction.RollbackAsync();
        res.UpdateValues("404", "The cinema does not have that film on the billboard", null, "Error");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var language = await _service.SearchLanguageAsync(showtimeBody.LanguageId);
      var format = await _service.SearchFormatAsync(showtimeBody.FormatId);
      if (language is null || format is null)
      {
        var message = language is null ? "Language not found" : "Format not found";
        res.UpdateValues("404", message, null, null);
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      if (_service.MovieContainFormatandLanguage(movie, format.Id, language.Id))
      {
        var EndDate = showtimeBody.StartDate.AddMinutes(movie.Duration);
        var IsOverlapping = await _service.IsOverlappingAsync(showtimeBody.StartDate, EndDate, theater.Id);
        if (IsOverlapping)
        {
          res.UpdateValues("409", $"There is a showtime between {showtimeBody.StartDate} and {EndDate}", null, "Conflict");
          await transaction.RollbackAsync();
          return StatusCode(StatusCodes.Status409Conflict, res);
        }
        else
        {
          Showtime showtimeDB = new Showtime
          {
            StartDate = showtimeBody.StartDate,
            FinishDate = EndDate,
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
      }
      else
      {
        res.UpdateValues("400", "the format or language of the feature does not exist in the movie", null, "Bad Request");
        return StatusCode(StatusCodes.Status400BadRequest, res);
      }
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