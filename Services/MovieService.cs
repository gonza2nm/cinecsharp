using backend_cine.Dbcontext;
using backend_cine.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Services;

public class MovieService(DbContextCinema dbContext)
{
  private readonly DbContextCinema _dbContext = dbContext;

  public async Task<List<Movie>> GetMoviesAsync()
  {
    return await _dbContext.Movies.ToListAsync();
  }
  public async Task<Movie?> GetMovieByIdAsync(long id, string? opt = null)
  {
    if (opt is not null)
    {
      if (opt.Equals("details", StringComparison.OrdinalIgnoreCase))
      {
        return await _dbContext.Movies.Include(m => m.Languages).Include(m => m.Formats).Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);
      }
      //agregar mas opciones si es necesario
    }
    return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
  }

  public async Task<List<Format>> SearchFormatsByIdsAsync(List<long> formatIdList)
  {
    return await _dbContext.Formats.Where(f => formatIdList.Contains(f.Id)).ToListAsync();
  }

  public async Task<List<Language>> SearchLanguagesByIdsAsync(List<long> languageIdList)
  {
    return await _dbContext.Languages.Where(l => languageIdList.Contains(l.Id)).ToListAsync();
  }

  public async Task<List<Genre>> SearchGenresByIdsAsync(List<long> genreIdList)
  {
    return await _dbContext.Genres.Where(g => genreIdList.Contains(g.Id)).ToListAsync();
  }


  public async Task UpdateGenres(Movie movie, List<long> genreIds)
  {
    foreach (var genreId in genreIds)
    {
      var genre = await _dbContext.Genres.FindAsync(genreId);
      if (genre != null && !movie.Genres.Contains(genre))
      {
        movie.Genres.Add(genre);
      }
    }
    movie.Genres.RemoveAll(g => !genreIds.Contains(g.Id));
  }


  public async Task UpdateFormats(Movie movie, List<long> formatIds)
  {
    foreach (var formatId in formatIds)
    {
      var format = await _dbContext.Formats.FindAsync(formatId);
      if (format != null && !movie.Formats.Contains(format))
      {
        movie.Formats.Add(format);
      }
    }
    movie.Formats.RemoveAll(f => !formatIds.Contains(f.Id));
  }

  public async Task UpdateLanguages(Movie movie, List<long> languageIds)
  {
    foreach (var languageId in languageIds)
    {
      var language = await _dbContext.Languages.FindAsync(languageId);
      if (language != null && !movie.Languages.Contains(language))
      {
        movie.Languages.Add(language);
      }
    }
    movie.Languages.RemoveAll(l => !languageIds.Contains(l.Id));

  }


}