using System.Text.Json.Serialization;

namespace backend_cine.Models;

public class Seat : BaseEntity
{
  public int Number { get; set; }
  public long RowId { get; set; }

  [JsonIgnore]
  public Row Row { get; set; } = null!;
}