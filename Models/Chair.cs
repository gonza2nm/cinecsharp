namespace backend_cine.Models;

public class Chair : BaseEntity
{
  public int ChairNumber { get; set; }
  public Row Row { get; set; } = null!;
  public long RowId { get; set; }
}