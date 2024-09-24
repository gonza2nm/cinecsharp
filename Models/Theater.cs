using System.Text.Json.Serialization;

namespace backend_cine.Models;

public class Theater : BaseEntity
{
  public required string TheaterName { get; set; }
  public long CinemaId { get; set; }
  public List<Showtime> Showtimes { get; set; } = new List<Showtime>();
  public List<Row> Rows { get; set; } = new List<Row>();

  [JsonIgnore]
  public Cinema Cinema { get; set; } = null!;
}