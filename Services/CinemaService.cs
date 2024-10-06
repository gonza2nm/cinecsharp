using System.IO.Compression;
using backend_cine.Dbcontext;
using backend_cine.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Services;

public class CinemaService(DbContextCinema dbContext)
{
  private readonly DbContextCinema _dbContext = dbContext;

  public async Task<List<Cinema>> GetCinemasAsync(List<string>? opt = null)
  {
    return await _dbContext.Cinemas.ToListAsync();
  }

  public async Task<Cinema?> GetCinemaByIdAsync(long id)
  {
    return await _dbContext.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
  }

  public async Task<Cinema?> GetCinemaByAddressAsync(string address)
  {
    return await _dbContext.Cinemas.FirstOrDefaultAsync(c => c.Address == address);
  }

  public async Task AddMoviesToCinemaAsync(Cinema cinema, List<long> movieIds)
  {
    if (movieIds != null && movieIds.Any())
    {
      var movies = await _dbContext.Movies.Where(m => movieIds.Contains(m.Id)).ToListAsync();
      cinema.Movies.AddRange(movies);
    }
  }

  public async Task UpdateMoviesInCinemaAsync(Cinema cinema, List<long> movieIds)
  {
    if (movieIds != null)
    {
      var moviesToRemove = cinema.Movies.Where(m => !movieIds.Contains(m.Id)).ToList();
      foreach (var movie in moviesToRemove)
      {
        cinema.Movies.Remove(movie);
      }

      var newMovieIds = movieIds.Except(cinema.Movies.Select(m => m.Id)).ToList();
      if (newMovieIds.Any())
      {
        var newMovies = await _dbContext.Movies.Where(m => newMovieIds.Contains(m.Id)).ToListAsync();
        cinema.Movies.AddRange(newMovies);
      }
    }
  }

}