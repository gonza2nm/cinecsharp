using System.ComponentModel.DataAnnotations;

namespace backend_cine.Models;

public abstract class BaseEntity
{
  [Key]
  public long Id { get; set; }
}