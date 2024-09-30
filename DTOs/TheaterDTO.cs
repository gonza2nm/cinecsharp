using backend_cine.Models;

namespace backend_cine.DTOs;
public class TheaterDTO
{
  public long Id { get; set; }
  public required string TheaterName { get; set; }
  public long CinemaId { get; set; }
  public List<Showtime> Showtimes { get; set; } = new List<Showtime>();
  public List<Row> Rows { get; set; } = new List<Row>();
  public Cinema Cinema { get; set; } = null!;
}

public class TheaterRequestDTO
{
  public long Id { get; set; }
  public required string TheaterName { get; set; }
  public required long CinemaId { get; set; }
  public int Rows { get; set; }
  public int MaxSeats { get; set; }
}
