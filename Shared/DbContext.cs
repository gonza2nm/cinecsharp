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
  public DbSet<Chair> Chairs { get; set; }

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
      tb.Property(c => c.Created).HasColumnType("DATETIME2(3)").HasDefaultValueSql("SYSDATETIME()");
      tb.Property(c => c.IsManager).HasDefaultValue(false);
      tb.Property(c => c.CinemaId).HasColumnName("cinema_id");
      tb.HasOne(c => c.Cinema).WithMany(c => c.Users).HasForeignKey(c => c.CinemaId).IsRequired();
      tb.HasMany(c => c.Purchases).WithOne(c => c.User).HasForeignKey(c => c.UserId);
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
      tb.HasMany(c => c.Showtimes).WithOne(c => c.Theater).HasForeignKey(c => c.TheaterId);
      tb.HasOne(c => c.Cinema).WithMany(c => c.Theaters).HasForeignKey(c => c.CinemaId);
      tb.HasMany(c => c.Rows).WithOne(c => c.Theater).HasForeignKey(c => c.TheaterId);
    });
    builder.Entity<Purchase>(tb =>
    {
      tb.ToTable("purchase");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.Total).HasPrecision(14, 2).HasColumnName("total");
      tb.Property(c => c.PurchaseDate).HasColumnType("DATETIME2(3)").HasDefaultValueSql("SYSDATETIME()");
      tb.HasOne(c => c.User).WithMany(c => c.Purchases).HasForeignKey(c => c.UserId);
      tb.HasMany(c => c.Tickets).WithOne(c => c.Purchase).HasForeignKey(c => c.PurchaseId);
    });
    builder.Entity<Showtime>(tb =>
    {
      tb.ToTable("showtime");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.DayAndHourStart).HasColumnType("DATETIME2(3)").HasDefaultValueSql("SYSDATETIME()");
      tb.HasOne(c => c.Movie).WithMany(c => c.Showtimes).HasForeignKey(c => c.MovieId);
      tb.HasOne(c => c.Language).WithMany(c => c.Showtimes).HasForeignKey(c => c.LanguageId);
      tb.HasOne(c => c.Format).WithMany(c => c.Showtimes).HasForeignKey(c => c.FormatId);
      tb.HasOne(c => c.Theater).WithMany(c => c.Showtimes).HasForeignKey(c => c.TheaterId);
      tb.HasMany(c => c.Tickets).WithOne(c => c.Showtime).HasForeignKey(c => c.ShowtimeId);
    });
    builder.Entity<Ticket>(tb =>
    {
      tb.ToTable("ticket");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.TicketNumber).HasColumnName("ticket_no");
      tb.HasOne(c => c.Showtime).WithMany(c => c.Tickets).HasForeignKey(c => c.ShowtimeId).OnDelete(DeleteBehavior.Restrict);
      tb.HasOne(c => c.Purchase).WithMany(c => c.Tickets).HasForeignKey(c => c.PurchaseId).OnDelete(DeleteBehavior.Restrict);
    });
    builder.Entity<Row>(tb =>
    {
      tb.ToTable("row");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.RowNumber).HasColumnName("row_no");
      tb.Property(c => c.TotalCapacity).HasColumnName("total_capacity");
      tb.HasOne(c => c.Theater).WithMany(c => c.Rows).HasForeignKey(c => c.TheaterId);
      tb.HasMany(c => c.Chairs).WithOne(c => c.Row).HasForeignKey(c => c.RowId);
    });
    builder.Entity<Chair>(tb =>
    {
      tb.ToTable("chair");
      tb.HasKey(c => c.Id);
      tb.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
      tb.Property(c => c.ChairNumber).HasColumnName("chair_no");
      tb.HasOne(c => c.Row).WithMany(c => c.Chairs).HasForeignKey(c => c.RowId);
    });

  }
  /* 
  1. ver como agregar la clase chair para tener un mejor control de cuantas sillas hay ocupadas
  2. dotnet ef migrations add PreFinalDatabase
  3. dotnet ef database update
  4. Create all Controllers and your CRUDs
  5. Agregar autenticacion y mejorar metodos
  */
}