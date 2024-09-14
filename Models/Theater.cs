namespace backend_cine.Models;

public class Theater : BaseEntity
{
  public string TheaterName { get; set; }
  public List<Showtime> Showtimes { get; set; } = new List<Showtime>();
  public long CinemaId { get; set; }
  public Cinema Cinema { get; set; } = null!;
  public List<Row> Rows { get; set; } = new List<Row>();
}