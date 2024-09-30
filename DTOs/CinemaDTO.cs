namespace backend_cine.DTOs;

public class CinemaDTO
{
  public long Id { get; set; }
  public required string Name { get; set; }
  public required string Address { get; set; }
}

public class CinemaRequestDTO
{
  public long Id { get; set; }
  public required string Name { get; set; }
  public required string Address { get; set; }
}