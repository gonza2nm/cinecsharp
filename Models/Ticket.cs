namespace backend_cine.Models;

public class Ticket : BaseEntity
{
  public int ticket_number;
  public long ShowtimeId { get; set; }
  public long PurchaseId { get; set; }
  public Showtime Showtime { get; set; } = null!;
  public Purchase Purchase { get; set; } = null!;
}