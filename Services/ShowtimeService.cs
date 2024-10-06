using System.IO.Compression;
using backend_cine.Dbcontext;
using backend_cine.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Services;

public class ShowtimeService(DbContextCinema dbContext)
{
  private readonly DbContextCinema _dbContext = dbContext;

  public async Task<bool> IsOverlappingAsync(DateTime startDate, DateTime finishDate, long theaterId)
  {
    return await _dbContext.Showtimes.AnyAsync(s => s.TheaterId == theaterId && s.StartDate < finishDate && s.FinishDate > startDate);
  }

  public bool MovieContainFormatandLanguage(Movie movie, long formatId, long languageId)
  {
    return movie.Formats.Any(f => f.Id == formatId) && movie.Languages.Any(l => l.Id == languageId);
  }
  public async Task<bool> CinemaContainMovieAsync(long theaterId, long movieId)
  {
    var cinema = await _dbContext.Cinemas.Include(c => c.Theaters).Include(c => c.Movies).Where(c => c.Theaters.Any(t => t.Id == theaterId) && c.Movies.Any(m => m.Id == movieId)).FirstOrDefaultAsync();
    if (cinema == null)
    {
      return false;
    }
    return true;
  }

  public async Task<List<Showtime>> GetShowtimesAsync(List<string>? opt = null)
  {
    //optiones es por si luego queremos agregarle algun filtro
    return await _dbContext.Showtimes.ToListAsync();
  }
  public async Task<Showtime?> GetOneShowtimeByIdAsync(long id)
  {
    return await _dbContext.Showtimes.FirstOrDefaultAsync(s => s.Id == id);
  }
  public async Task<Movie?> SearchMovieAsync(long id)
  {
    return await _dbContext.Movies.Include(m => m.Languages).Include(m => m.Formats).FirstOrDefaultAsync(m => m.Id == id);
  }
  public async Task<Theater?> SearchTheaterAsync(long id)
  {
    return await _dbContext.Theaters.FirstOrDefaultAsync(t => t.Id == id);
  }
  public async Task<Language?> SearchLanguageAsync(long id)
  {
    return await _dbContext.Languages.FirstOrDefaultAsync(l => l.Id == id);
  }
  public async Task<Format?> SearchFormatAsync(long id)
  {
    return await _dbContext.Formats.FirstOrDefaultAsync(f => f.Id == id);
  }

}