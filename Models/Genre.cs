using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace backend_cine.Models;

public class Genre : BaseEntity
{
  public required string Name { get; set; }
  public List<Movie> Movies { get; set; } = new List<Movie>();
}