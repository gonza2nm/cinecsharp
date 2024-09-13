namespace backend_cine.Models;

public class Language : BaseEntity
{
  public string Name { get; set; }
  public List<Movie> Movies = [];
  public List<Showtime> Showtimes = new List<Showtime>();
}