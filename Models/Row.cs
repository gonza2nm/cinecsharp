using System.Text.Json.Serialization;

namespace backend_cine.Models;

public class Row : BaseEntity
{
  public int RowNumber { get; set; }
  public int TotalCapacity { get; set; }
  public long TheaterId { get; set; }
  public List<Seat> Seats { get; set; } = new List<Seat>();

  [JsonIgnore]
  public Theater Theater { get; set; } = null!;
}