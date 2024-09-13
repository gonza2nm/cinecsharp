namespace backend_cine.Models;

public class User : BaseEntity
{
    public string Dni { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime Created { get; set; }
    public string Password { get; set; }
    public bool IsManager { get; set; }
    public long CinemaId { get; set; }
    public Cinema Cinema { get; set; } = null!;
    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
}