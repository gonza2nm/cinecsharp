namespace backend_cine.DTOs;

public class UserDTO
{
  public required string Dni { get; set; }
  public required string Name { get; set; }
  public required string Surname { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
}
public class UserRequestDTO
{
  public required string Dni { get; set; }
  public required string Name { get; set; }
  public required string Surname { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
  public long CinemaId { get; set; }
}