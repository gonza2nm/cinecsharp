namespace backend_cine.Models;

public class Format : BaseEntity
{
  public string Name { get; set; }
  public List<Movie> Movies = new List<Movie>();
  public List<Showtime> Showtimes = new List<Showtime>();
}