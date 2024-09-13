using System.IO.Compression;
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
      tb.ToTable("cinema");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.Name).HasColumnName("cinema_name").HasColumnType("varchar(50)");
      tb.Property(c => c.Address).HasColumnName("address").HasColumnType("varchar(80)");
      tb.HasMany(c => c.Users).WithOne(c => c.Cinema).HasForeignKey(c => c.CinemaId).IsRequired(false);
      tb.HasMany(c => c.Theaters).WithOne(c => c.Cinema).HasForeignKey(c => c.CinemaId);
      tb.HasMany(c => c.Movies).WithMany(c => c.Cinemas);
    });
    builder.Entity<User>(tb =>
    {
      tb.ToTable("user");
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
    builder.Entity<Movie>(tb =>
    {
      tb.ToTable("movie");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.Name).HasMaxLength(50);
      tb.Property(c => c.Description).HasMaxLength(200);
      tb.HasMany(c => c.Cinemas).WithMany(c => c.Movies);
      tb.HasMany(c => c.Formats).WithMany(c => c.Movies);
      tb.HasMany(c => c.Languages).WithMany(c => c.Movies);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Movie).HasForeignKey(c => c.MovieId);
    });
    builder.Entity<Format>(tb =>
    {
      tb.ToTable("format");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.Name).HasMaxLength(40);
      tb.HasMany(c => c.Movies).WithMany(c => c.Formats);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Format).HasForeignKey(c => c.FormatId);
    });
    builder.Entity<Language>(tb =>
    {
      tb.ToTable("language");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.Name).HasMaxLength(40);
      tb.HasMany(c => c.Movies).WithMany(c => c.Languages);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Language).HasForeignKey(c => c.LanguageId);
    });
    builder.Entity<Theater>(tb =>
    {
      tb.ToTable("theater");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.TheaterName).HasMaxLength(5).HasColumnName("theater_name");
      tb.Property(c => c.Chairs);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Theater).HasForeignKey(c => c.TheaterId);
      tb.HasOne(c => c.Cinema).WithMany(c => c.Theaters).HasForeignKey(c => c.CinemaId);
    });
    /*Modificar entidad Purchase, Showtime, Ticket, User and do 
    1. ver como agregar la clase chair para tener un mejor control de cuantas sillas hay ocupadas
    2. dotnet ef migrations add PreFinalDatabase
    3. dotnet ef database update
    4. Create all Controllers and your CRUDs
    5. Agregar autenticacion y mejorar metodos
    */
  }
  public DbSet<Cinema> Cinemas { get; set; }
  public DbSet<User> Users { get; set; }

}