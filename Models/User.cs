namespace backend_cine.Models;

public class User
{
    public int Id { get; set; }
    public required string Dni { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool IsManager { get; set; }
}