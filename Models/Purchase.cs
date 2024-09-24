using System.Text.Json.Serialization;

namespace backend_cine.Models;

public class Purchase : BaseEntity
{
  public DateTime PurchaseDate { get; set; }
  public decimal Total { get; set; }
  public long UserId { get; set; }
  public List<Ticket> Tickets { get; set; } = new List<Ticket>();

  [JsonIgnore]
  public User User { get; set; } = null!;
}