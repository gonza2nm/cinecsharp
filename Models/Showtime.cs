namespace backend_cine.Models;

public class Showtime : BaseEntity
{
  public DateTime StartDate { get; set; }
  public DateTime FinishDate { get; set; }
  public long MovieId { get; set; }
  public long LanguageId { get; set; }
  public long FormatId { get; set; }
  public long TheaterId { get; set; }
  public Movie Movie { get; set; } = null!;
  public Language Language { get; set; } = null!;
  public Format Format { get; set; } = null!;
  public Theater Theater { get; set; } = null!;
  public List<Ticket> Tickets { get; set; } = new List<Ticket>();
}