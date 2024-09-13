using backend_cine.Models;
using Microsoft.EntityFrameworkCore;


namespace backend_cine.Dbcontext;

public sealed class DbContextCinema : DbContext
{
  public DbContextCinema(DbContextOptions<DbContextCinema> options) : base(options)
  { }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema("gcinema");
    builder.Entity<Cinema>(tb =>
    {
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.Name).HasColumnName("cinema_name").HasColumnType("varchar(50)");
      tb.HasMany(c => c.Users).WithOne(c => c.Cinema).HasForeignKey(c => c.CinemaId).IsRequired(false);
      tb.Property(c => c.Address).HasColumnName("address").HasColumnType("varchar(80)");
    });
    builder.Entity<User>(tb =>
    {
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.Name).HasMaxLength(30);
      tb.Property(c => c.Surname).HasMaxLength(30);
      tb.HasIndex(c => c.Dni).IsUnique();
      tb.Property(c => c.Dni).HasColumnType("varchar(12)");
      tb.Property(c => c.Email).HasMaxLength(50);
      tb.Property(c => c.Password).HasMaxLength(16);
      tb.Property(c => c.Created).HasDefaultValueSql("getdate()");
      tb.Property(c => c.IsManager).HasDefaultValue(false);
      tb.Property(c => c.CinemaId).HasColumnName("cinema_id");
      tb.HasOne(c => c.Cinema).WithMany(c => c.Users).HasForeignKey(c => c.CinemaId).IsRequired();
    });
  }
  public DbSet<Cinema> Cinemas { get; set; }
  public DbSet<User> Users { get; set; }

}