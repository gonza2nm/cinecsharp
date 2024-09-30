using backend_cine.Models;

namespace backend_cine.DTOs;

public class MovieDTO
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required int Duration { get; set; }
  public required string Director { get; set; }
  public List<Format> Formats { get; } = new List<Format>();
  public List<Language> Languages { get; } = new List<Language>();
  public List<Genre> Genres { get; } = new List<Genre>();
}

public class MovieRequestDTO
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public string? Description { get; set; }
  public required int Duration { get; set; }
  public required string Director { get; set; }
  public List<long> FormatsIds { get; set; } = new List<long>();
  public List<long> LanguagesIds { get; set; } = new List<long>();
  public List<long> GenresIds { get; set; } = new List<long>();

}