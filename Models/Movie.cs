namespace backend_cine.Models;

public class Movie : BaseEntity
{
  public required string Name { get; set; }
  public string? Description { get; set; }
  public List<Cinema> Cinemas { get; } = new List<Cinema>();
  public List<Format> Formats { get; } = new List<Format>();
  public List<Language> Languages { get; } = new List<Language>();
  public List<Showtime> Showtimes = new List<Showtime>();
}