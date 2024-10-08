using backend_cine.Models;
using Microsoft.EntityFrameworkCore;


namespace backend_cine.Dbcontext;

public sealed class DbContextCinema : DbContext
{
  public DbContextCinema(DbContextOptions<DbContextCinema> options) : base(options)
  { }
  public DbSet<Cinema> Cinemas { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Movie> Movies { get; set; }
  public DbSet<Format> Formats { get; set; }
  public DbSet<Language> Languages { get; set; }
  public DbSet<Theater> Theaters { get; set; }
  public DbSet<Purchase> Purchases { get; set; }
  public DbSet<Showtime> Showtimes { get; set; }
  public DbSet<Ticket> Tickets { get; set; }
  public DbSet<Row> Rows { get; set; }
  public DbSet<Seat> Seats { get; set; }
  public DbSet<Genre> Genres { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.Entity<Cinema>(tb =>
    {
      tb.Property(c => c.Name).HasColumnType("varchar(50)");
      tb.Property(c => c.Address).HasColumnType("varchar(80)");
      tb.HasIndex(c => c.Address).IsUnique();
      tb.HasMany(c => c.Users).WithOne(c => c.Cinema).HasForeignKey(c => c.CinemaId).IsRequired(false);
      tb.HasMany(c => c.Theaters).WithOne(c => c.Cinema).HasForeignKey(c => c.CinemaId);
      tb.HasMany(c => c.Movies).WithMany(c => c.Cinemas);
      tb.HasData(
        new Cinema { Id = 1, Name = "Cinepolis", Address = "Av. Eva Peron 5856, Rosario, Santa Fe" },
        new Cinema { Id = 2, Name = "Showcase", Address = "Junin 501, Rosario, Santa Fe" },
        new Cinema { Id = 3, Name = "CineUTN", Address = "Zeballos 1341, Rosario, Santa Fe" }
      );
    });
    builder.Entity<User>(tb =>
    {
      tb.Property(c => c.Name).HasMaxLength(30);
      tb.Property(c => c.Surname).HasMaxLength(30);
      tb.HasIndex(c => c.Dni).IsUnique();
      tb.Property(c => c.Dni).HasColumnType("varchar(12)");
      tb.Property(c => c.Email).HasMaxLength(50);
      tb.HasIndex(c => c.Email).IsUnique();
      tb.Property(c => c.Password).HasMaxLength(16);
      tb.Property(c => c.Created).HasColumnType("DATETIME2(0)").HasDefaultValueSql("SYSDATETIME()");
      tb.Property(c => c.IsManager).HasDefaultValue(false);
      tb.Property(c => c.CinemaId);
      tb.HasOne(c => c.Cinema).WithMany(c => c.Users).HasForeignKey(c => c.CinemaId);
      tb.HasMany(c => c.Purchases).WithOne(c => c.User).HasForeignKey(c => c.UserId);
    });
    builder.Entity<Movie>(tb =>
    {
      tb.Property(c => c.Name).HasMaxLength(50);
      tb.Property(c => c.Description).HasMaxLength(200);
      tb.Property(c => c.Duration);
      tb.Property(c => c.Director).HasMaxLength(50);
      tb.HasMany(c => c.Cinemas).WithMany(c => c.Movies);
      tb.HasMany(c => c.Formats).WithMany(c => c.Movies);
      tb.HasMany(c => c.Languages).WithMany(c => c.Movies);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Movie).HasForeignKey(c => c.MovieId);
      tb.HasMany(c => c.Genres).WithMany(c => c.Movies);
    });
    builder.Entity<Format>(tb =>
    {
      tb.Property(c => c.Name).HasMaxLength(40);
      tb.HasIndex(c => c.Name).IsUnique();
      tb.HasMany(c => c.Movies).WithMany(c => c.Formats);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Format).HasForeignKey(c => c.FormatId);
      tb.HasData(
        new Format { Id = 1, Name = "2D" },
        new Format { Id = 2, Name = "3D" },
        new Format { Id = 3, Name = "4D" }
      );
    });
    builder.Entity<Language>(tb =>
    {
      tb.Property(c => c.Name).HasMaxLength(40);
      tb.HasIndex(c => c.Name).IsUnique();
      tb.HasMany(c => c.Movies).WithMany(c => c.Languages);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Language).HasForeignKey(c => c.LanguageId);
      tb.HasData(
        new Language { Id = 1, Name = "Spanish" },
        new Language { Id = 2, Name = "English" },
        new Language { Id = 3, Name = "Chinese" }
      );
    });
    builder.Entity<Theater>(tb =>
    {
      tb.Property(c => c.TheaterName).HasMaxLength(20);
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Theater).HasForeignKey(c => c.TheaterId);
      tb.HasOne(c => c.Cinema).WithMany(c => c.Theaters).HasForeignKey(c => c.CinemaId);
      tb.HasMany(c => c.Rows).WithOne(c => c.Theater).HasForeignKey(c => c.TheaterId);
    });
    builder.Entity<Purchase>(tb =>
    {
      tb.Property(c => c.Total).HasPrecision(14, 2);
      tb.Property(c => c.PurchaseDate).HasColumnType("DATETIME2(0)").HasDefaultValueSql("SYSDATETIME()");
      tb.HasOne(c => c.User).WithMany(c => c.Purchases).HasForeignKey(c => c.UserId);
      tb.HasMany(c => c.Tickets).WithOne(c => c.Purchase).HasForeignKey(c => c.PurchaseId);
    });
    builder.Entity<Showtime>(tb =>
    {
      tb.Property(c => c.StartDate).HasColumnType("DATETIME2(0)").HasDefaultValueSql("SYSDATETIME()");
      tb.Property(c => c.FinishDate).HasColumnType("DATETIME2(0)").HasDefaultValueSql("SYSDATETIME()");
      tb.HasOne(c => c.Movie).WithMany(c => c.Showtimes).HasForeignKey(c => c.MovieId);
      tb.HasOne(c => c.Language).WithMany(c => c.Showtimes).HasForeignKey(c => c.LanguageId);
      tb.HasOne(c => c.Format).WithMany(c => c.Showtimes).HasForeignKey(c => c.FormatId);
      tb.HasOne(c => c.Theater).WithMany(c => c.Showtimes).HasForeignKey(c => c.TheaterId);
      tb.HasMany(c => c.Tickets).WithOne(c => c.Showtime).HasForeignKey(c => c.ShowtimeId);
    });
    builder.Entity<Ticket>(tb =>
    {
      tb.Property(c => c.TicketNumber);
      tb.HasOne(c => c.Showtime).WithMany(c => c.Tickets).HasForeignKey(c => c.ShowtimeId).OnDelete(DeleteBehavior.Restrict);
      tb.HasOne(c => c.Purchase).WithMany(c => c.Tickets).HasForeignKey(c => c.PurchaseId).OnDelete(DeleteBehavior.Restrict);
    });
    builder.Entity<Row>(tb =>
    {
      tb.Property(c => c.RowNumber);
      tb.Property(c => c.TotalCapacity);
      tb.HasOne(c => c.Theater).WithMany(c => c.Rows).HasForeignKey(c => c.TheaterId);
      tb.HasMany(c => c.Seats).WithOne(c => c.Row).HasForeignKey(c => c.RowId);
    });
    builder.Entity<Seat>(tb =>
    {
      tb.Property(c => c.Number);
      tb.HasOne(c => c.Row).WithMany(c => c.Seats).HasForeignKey(c => c.RowId);
    });
    builder.Entity<Genre>(tb =>
    {
      tb.Property(c => c.Name).HasMaxLength(20);
      tb.HasMany(c => c.Movies).WithMany(c => c.Genres);
      tb.HasData(
        new Genre { Id = 1, Name = "Terror" },
        new Genre { Id = 2, Name = "Comedia" },
        new Genre { Id = 3, Name = "Accion" },
        new Genre { Id = 4, Name = "Drama" },
        new Genre { Id = 5, Name = "Ciencia Ficcion" },
        new Genre { Id = 6, Name = "Documental" },
        new Genre { Id = 7, Name = "Suspenso" }
      );
    });

  }
  /* 
  4. Create all Controllers and your CRUDs
  5. Agregar autenticacion y mejorar metodos
  */
}