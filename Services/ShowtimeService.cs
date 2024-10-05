using backend_cine.Dbcontext;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Services;

public class ShowtimeService(DbContextCinema dbContext)
{
  private readonly DbContextCinema _dbContext = dbContext;

  public async Task<bool> IsOverlapping(DateTime startDate, DateTime finishDate, long theaterId)
  {
    return await _dbContext.Showtimes.AnyAsync(s => s.TheaterId == theaterId && s.StartDate < finishDate && s.FinishDate > startDate);
  }
}