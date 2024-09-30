namespace backend_cine.Models;

public class Movie : BaseEntity
{
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required int Duration { get; set; }
  public required string Director { get; set; }
  public List<Cinema> Cinemas { get; set; } = new List<Cinema>();
  public List<Format> Formats { get; set; } = new List<Format>();
  public List<Language> Languages { get; set; } = new List<Language>();
  public List<Showtime> Showtimes { get; set; } = new List<Showtime>();
  public List<Genre> Genres { get; set; } = new List<Genre>();
}