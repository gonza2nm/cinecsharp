namespace backend_cine.Models;

public class Row : BaseEntity
{
  public int RowNumber { get; set; }
  public int TotalCapacity { get; set; }
  public long TheaterId { get; set; }
  public Theater Theater { get; set; } = null!;
  public List<Chair> Chairs { get; set; } = new List<Chair>();
}