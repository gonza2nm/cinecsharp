namespace backend_cine.Models;

public class Seat : BaseEntity
{
  public int Number { get; set; }
  public Row Row { get; set; } = null!;
  public long RowId { get; set; }
}