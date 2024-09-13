using System.ComponentModel.DataAnnotations.Schema;

namespace backend_cine.Models;
[Table("cinemas")]
public class Cinema : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
}
