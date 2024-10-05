using backend_cine.Dbcontext;

namespace backend_cine.Services;

public class UserService(DbContextCinema dbContext)
{
  private readonly DbContextCinema _dbContext = dbContext;
}