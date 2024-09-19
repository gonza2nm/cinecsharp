namespace backend_cine.Models;

public class Language : BaseEntity
{
  public required string Name { get; set; }
  public List<Movie> Movies = new List<Movie>();
  public List<Showtime> Showtimes = new List<Showtime>();
}