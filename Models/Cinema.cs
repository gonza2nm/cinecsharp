namespace backend_cine.Models;

public class Cinema : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<User> Users { get; set; } = new List<User>();
    public List<Movie> Movies { get; } = new List<Movie>();
    public List<Theater> Theaters { get; set; } = new List<Theater>();
}
