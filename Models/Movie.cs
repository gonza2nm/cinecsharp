namespace backend_cine.Models;

public class Movie : BaseEntity
{
  public string Name { get; set; }
  public string? Description { get; set; }
  public List<Cinema> Cinemas { get; } = [];
  public List<Format> Formats { get; } = [];
  public List<Language> Languages { get; } = [];
  public List<Showtime> Showtimes = new List<Showtime>();
}