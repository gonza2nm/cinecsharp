namespace backend_cine.Models;

public class Purchase : BaseEntity
{
  public DateTime PurchaseDate { get; set; }
  public decimal TotalAmount { get; set; }
  public long UserId { get; set; }
  public User User { get; set; } = null!;
  public List<Ticket> Tickets { get; set; } = new List<Ticket>();
}