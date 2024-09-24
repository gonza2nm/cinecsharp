using System.Text.Json.Serialization;

namespace backend_cine.Models;

public class Ticket : BaseEntity
{
  public int TicketNumber;
  public long ShowtimeId { get; set; }
  public long PurchaseId { get; set; }

  [JsonIgnore]
  public Showtime Showtime { get; set; } = null!;
  [JsonIgnore]
  public Purchase Purchase { get; set; } = null!;
}