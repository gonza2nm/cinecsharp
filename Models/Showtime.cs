using System.Text.Json.Serialization;

namespace backend_cine.Models;

public class Showtime : BaseEntity
{
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