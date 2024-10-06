using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_cine.Models;
using backend_cine.Dbcontext;
using backend_cine.Interfaces;
using AutoMapper;
using backend_cine.DTOs;
using System.Runtime.CompilerServices;
using backend_cine.Services;

namespace backend_cine.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController(DbContextCinema dbContext, IMapper mapper, MovieService service) : ControllerBase, IRepository<MovieDTO, MovieRequestDTO>
{
  private readonly DbContextCinema _dbcontext = dbContext;
  private readonly IMapper _mapper = mapper;
  private readonly MovieService _service = service;

  //GET ALL
  [HttpGet]
  public async Task<ActionResult<ResponseList<MovieDTO>>> FindAll()
  {
    var res = new ResponseList<MovieDTO> { Status = "", Message = "", Data = [], Error = null };
    try
    {
      var moviesDB = await _service.GetMoviesAsync();
      var movies = _mapper.Map<List<MovieDTO>>(moviesDB);
      res.UpdateValues("200", "Found movies", movies);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (Exception ex)
    {
      res.UpdateValues("500", "An error occurred while processing your request.", [], ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }
  //GET ONE
  [HttpGet("{id}")]
  public async Task<ActionResult<ResponseOne<MovieDTO>>> FindOne(long id)
  {
    var res = new ResponseOne<MovieDTO> { Status = "", Message = "", Data = null, Error = null };
    if (id <= 0)
    {
      res.UpdateValues("400", "Invalid Movie ID", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    try
    {
      Movie? movieDB = await _service.GetMovieByIdAsync(id, "details");
      if (movieDB is null)
      {
        res.UpdateValues("404", $"Movie with id: {id} not found", null, "Not Found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var movieDTO = _mapper.Map<MovieDTO>(movieDB);
      res.UpdateValues("200", "Found movie", movieDTO);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (Exception ex)
    {
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

  //ADD
  [HttpPost]
  public async Task<ActionResult<ResponseOne<MovieDTO>>> Add([FromBody] MovieRequestDTO movieBody)
  {
    var res = new ResponseOne<MovieDTO> { Status = "", Message = "", Data = null, Error = null };
    if (movieBody is null || !ModelState.IsValid)
    {
      res.UpdateValues("400", "Incorrect or empty data", null, "Invalid data");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbcontext.Database.BeginTransactionAsync();
    try
    {
      var movieToAdd = _mapper.Map<Movie>(movieBody);
      movieToAdd.Languages = await _service.SearchLanguagesByIdsAsync(movieBody.LanguagesIds);
      movieToAdd.Formats = await _service.SearchFormatsByIdsAsync(movieBody.FormatsIds);
      movieToAdd.Genres = await _service.SearchGenresByIdsAsync(movieBody.GenresIds);
      await _dbcontext.Movies.AddAsync(movieToAdd);
      await _dbcontext.SaveChangesAsync();
      await transaction.CommitAsync();
      var movie = _mapper.Map<MovieDTO>(movieToAdd);
      res.UpdateValues("201", "Movie successfully created", movie);
      return StatusCode(StatusCodes.Status201Created, res);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<ResponseOne<MovieDTO>>> Update(long id, MovieRequestDTO movieBody)
  {
    var res = new ResponseOne<MovieRequestDTO> { Status = "", Message = "", Data = null, Error = null };
    if (!ModelState.IsValid || id <= 0)
    {
      res.UpdateValues("400", "Incorrect or empty data", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    if (id != movieBody.Id)
    {
      res.UpdateValues("400", "The id of the URI is diferent from the id in json object", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbcontext.Database.BeginTransactionAsync();
    try
    {
      var updateMovie = await _service.GetMovieByIdAsync(id, "details");
      if (updateMovie is null)
      {
        res.UpdateValues("404", $"Movie with id: {id} not found", null, "Not found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      _mapper.Map(movieBody, updateMovie);
      await _service.UpdateLanguages(updateMovie, movieBody.LanguagesIds);
      await _service.UpdateFormats(updateMovie, movieBody.FormatsIds);
      await _service.UpdateGenres(updateMovie, movieBody.GenresIds);
      _dbcontext.Movies.Update(updateMovie);
      await _dbcontext.SaveChangesAsync();
      await transaction.CommitAsync();
      res.UpdateValues("200", "Movie updated successfully", null);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<ResponseOne<MovieDTO>>> Delete(long id)
  {
    var res = new ResponseOne<MovieDTO> { Status = "", Message = "", Data = null, Error = null };
    if (id <= 0)
    {
      res.UpdateValues("400", "Invalid Movie ID", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbcontext.Database.BeginTransactionAsync();
    try
    {
      var deleteMovie = await _service.GetMovieByIdAsync(id);
      if (deleteMovie is null)
      {
        res.UpdateValues("404", $"Movie with id: {id} not found", null, "404 Not found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      _dbcontext.Movies.Remove(deleteMovie);
      await _dbcontext.SaveChangesAsync();
      await transaction.CommitAsync();
      res.UpdateValues("200", "Movie deleted successfully", null);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

}