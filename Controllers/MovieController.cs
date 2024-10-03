using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_cine.Models;
using backend_cine.Dbcontext;
using backend_cine.Interfaces;
using AutoMapper;
using backend_cine.DTOs;

namespace backend_cine.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController(DbContextCinema dbContext, IMapper mapper) : ControllerBase, IRepository<MovieDTO, MovieRequestDTO>
{
  private readonly DbContextCinema _dbcontext = dbContext;
  private readonly IMapper _mapper = mapper;

  //GET ALL
  [HttpGet]
  public async Task<ActionResult<ResponseList<MovieDTO>>> FindAll()
  {
    var res = new ResponseList<MovieDTO> { Status = "", Message = "", Data = [], Error = null };
    try
    {
      var moviesDB = await _dbcontext.Movies.ToListAsync();
      var movies = _mapper.Map<List<MovieDTO>>(moviesDB);
      res.UpdateValues("200", "Found movies", movies);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (DbUpdateException dbEx)
    {
      res.UpdateValues("500", "Database error occurred.", [], dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
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
      Movie? movieDB = await _dbcontext.Movies
        .Include(m => m.Languages)
        .Include(m => m.Formats)
        .Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);
      if (movieDB is null)
      {
        res.UpdateValues("404", $"Movie with id: {id} not found", null, "Not Found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var movieDTO = _mapper.Map<MovieDTO>(movieDB);
      res.UpdateValues("200", "Found movie", movieDTO);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (DbUpdateException dbEx)
    {
      res.UpdateValues("500", "Database error occurred.", null, dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
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
      movieToAdd.Languages = await _dbcontext.Languages.Where(l => movieBody.LanguagesIds.Contains(l.Id)).ToListAsync();
      movieToAdd.Formats = await _dbcontext.Formats.Where(f => movieBody.FormatsIds.Contains(f.Id)).ToListAsync();
      movieToAdd.Genres = await _dbcontext.Genres.Where(g => movieBody.GenresIds.Contains(g.Id)).ToListAsync();
      await _dbcontext.Movies.AddAsync(movieToAdd);
      await _dbcontext.SaveChangesAsync();
      await transaction.CommitAsync();
      var movie = _mapper.Map<MovieDTO>(movieToAdd);
      res.UpdateValues("201", "Movie successfully created", movie);
      return StatusCode(StatusCodes.Status201Created, res);
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
      Movie? updateMovie = await _dbcontext.Movies
        .Include(m => m.Formats)
        .Include(m => m.Languages)
        .Include(m => m.Genres)
        .FirstOrDefaultAsync(c => c.Id == id);
      if (updateMovie is null)
      {
        res.UpdateValues("404", $"Movie with id: {id} not found", null, "Not found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      _mapper.Map(movieBody, updateMovie);
      var existingFormats = updateMovie.Formats.ToList();
      var newFormatIds = movieBody.FormatsIds;
      // elimino formatos que no estan en la lista
      var formatsToRemove = existingFormats
          .Where(f => !newFormatIds.Contains(f.Id))
          .ToList();
      foreach (var format in formatsToRemove)
      {
        updateMovie.Formats.Remove(format);
      }
      foreach (var formatId in newFormatIds)
      {
        if (!existingFormats.Any(f => f.Id == formatId))
        {
          var formatToAdd = await _dbcontext.Formats.FindAsync(formatId);
          if (formatToAdd != null)
          {
            updateMovie.Formats.Add(formatToAdd);
          }
        }
      }
      // control generos
      var existingGenres = updateMovie.Genres.ToList();
      var newGenreIds = movieBody.GenresIds;
      // elimino géneros que no estan en la lista
      var genresToRemove = existingGenres
          .Where(g => !newGenreIds.Contains(g.Id))
          .ToList();
      foreach (var genre in genresToRemove)
      {
        updateMovie.Genres.Remove(genre);
      }
      // agrego los nuevos generos
      foreach (var genreId in newGenreIds)
      {
        if (!existingGenres.Any(g => g.Id == genreId))
        {
          var genreToAdd = await _dbcontext.Genres.FindAsync(genreId);
          if (genreToAdd != null)
          {
            updateMovie.Genres.Add(genreToAdd);
          }
        }
      }
      //control de lenguajes
      var existingLanguages = updateMovie.Languages.ToList();
      var newLanguageIds = movieBody.LanguagesIds;
      // elimino lenguajes que no estan en la lista
      var languagesToRemove = existingLanguages.Where(l => !newLanguageIds.Contains(l.Id)).ToList();
      foreach (var language in languagesToRemove)
      {
        updateMovie.Languages.Remove(language);
      }
      // Agrega los nuevos lenguajes
      foreach (var languageId in newLanguageIds)
      {
        if (!existingLanguages.Any(l => l.Id == languageId))
        {
          var languageToAdd = await _dbcontext.Languages.FindAsync(languageId);
          if (languageToAdd != null)
          {
            updateMovie.Languages.Add(languageToAdd);
          }
        }
      }
      _dbcontext.Movies.Update(updateMovie);
      await _dbcontext.SaveChangesAsync();
      await transaction.CommitAsync();
      res.UpdateValues("200", "Movie updated successfully", null);
      return StatusCode(StatusCodes.Status200OK, res);
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
      Movie? deleteMovie = await _dbcontext.Movies.FirstOrDefaultAsync(c => c.Id == id);
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

}