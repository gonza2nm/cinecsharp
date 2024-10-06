using backend_cine.Dbcontext;
using backend_cine.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Services;

public class UserService(DbContextCinema dbContext)
{
  private readonly DbContextCinema _dbContext = dbContext;

  public async Task<List<User>> GetUsersAsync()
  {
    return await _dbContext.Users.ToListAsync();
  }

  public async Task<User?> GetUserByIdAsync(long id)
  {
    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
  }
  public async Task<User?> GetUserByEmailAsync(string email)
  {
    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
  }
  public async Task<User?> GetUserByDniAsync(string dni)
  {
    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Dni == dni);
  }

  public async Task<User?> GetUserByEmailOrDniAsync(string email, string dni)
  {
    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email || u.Dni == dni);
  }

}