namespace backend_cine.Models;

public class User : BaseEntity
{
    public required string Dni { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public DateTime Created { get; set; }
    public required string Password { get; set; }
    public bool IsManager { get; set; }
    public long CinemaId { get; set; }
    public Cinema Cinema { get; set; } = null!;
    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
}