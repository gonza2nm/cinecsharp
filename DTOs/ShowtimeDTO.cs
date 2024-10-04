using System.Text.Json.Serialization;
using backend_cine.Models;

namespace backend_cine.DTOs;

public class ShowtimeDTO
{
  public long Id { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime FinishDate { get; set; }
  public long MovieId { get; set; }
  public long LanguageId { get; set; }
  public long FormatId { get; set; }
  public long TheaterId { get; set; }
  public List<Ticket> Tickets { get; set; } = new List<Ticket>();
  [JsonIgnore]
  public Movie Movie { get; set; } = null!;
  [JsonIgnore]
  public Language Language { get; set; } = null!;
  [JsonIgnore]
  public Format Format { get; set; } = null!;
  [JsonIgnore]
  public Theater Theater { get; set; } = null!;
}

public class ShowtimeRequestDTO
{
  public long Id { get; set; }
  public required DateTime StartDate { get; set; }
  public required long MovieId { get; set; }
  public required long LanguageId { get; set; }
  public required long FormatId { get; set; }
  public required long TheaterId { get; set; }
}