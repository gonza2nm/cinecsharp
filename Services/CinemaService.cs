using System.IO.Compression;
using backend_cine.Dbcontext;
using backend_cine.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Services;

public class CinemaService(DbContextCinema dbContext)
{
  private readonly DbContextCinema _dbContext = dbContext;

  public async Task<List<Cinema>> GetCinemasAsync(List<string>? opt)
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

}