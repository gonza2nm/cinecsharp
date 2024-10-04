﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend_cine.Dbcontext;

#nullable disable

namespace backend_cine.Migrations
{
    [DbContext(typeof(DbContextCinema))]
    [Migration("20241003143058_NullCinemaInUser")]
    partial class NullCinemaInUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CinemaMovie", b =>
                {
                    b.Property<long>("CinemasId")
                        .HasColumnType("bigint");

                    b.Property<long>("MoviesId")
                        .HasColumnType("bigint");

                    b.HasKey("CinemasId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CinemaMovie");
                });

            modelBuilder.Entity("FormatMovie", b =>
                {
                    b.Property<long>("FormatsId")
                        .HasColumnType("bigint");

                    b.Property<long>("MoviesId")
                        .HasColumnType("bigint");

                    b.HasKey("FormatsId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("FormatMovie");
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.Property<long>("GenresId")
                        .HasColumnType("bigint");

                    b.Property<long>("MoviesId")
                        .HasColumnType("bigint");

                    b.HasKey("GenresId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("GenreMovie");
                });

            modelBuilder.Entity("LanguageMovie", b =>
                {
                    b.Property<long>("LanguagesId")
                        .HasColumnType("bigint");

                    b.Property<long>("MoviesId")
                        .HasColumnType("bigint");

                    b.HasKey("LanguagesId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("LanguageMovie");
                });

            modelBuilder.Entity("backend_cine.Models.Cinema", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.ToTable("Cinemas");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Address = "Av. Eva Peron 5856, Rosario, Santa Fe",
                            Name = "Cinepolis"
                        },
                        new
                        {
                            Id = 2L,
                            Address = "Junin 501, Rosario, Santa Fe",
                            Name = "Showcase"
                        },
                        new
                        {
                            Id = 3L,
                            Address = "Zeballos 1341, Rosario, Santa Fe",
                            Name = "CineUTN"
                        });
                });

            modelBuilder.Entity("backend_cine.Models.Format", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Formats");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "2D"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "3D"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "4D"
                        });
                });

            modelBuilder.Entity("backend_cine.Models.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Terror"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Comedia"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Accion"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Drama"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Ciencia Ficcion"
                        },
                        new
                        {
                            Id = 6L,
                            Name = "Documental"
                        },
                        new
                        {
                            Id = 7L,
                            Name = "Suspenso"
                        });
                });

            modelBuilder.Entity("backend_cine.Models.Language", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Spanish"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "English"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Chinese"
                        });
                });

            modelBuilder.Entity("backend_cine.Models.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("backend_cine.Models.Purchase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("PurchaseDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME2(3)")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<decimal>("Total")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("backend_cine.Models.Row", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("RowNumber")
                        .HasColumnType("int");

                    b.Property<long>("TheaterId")
                        .HasColumnType("bigint");

                    b.Property<int>("TotalCapacity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheaterId");

                    b.ToTable("Rows");
                });

            modelBuilder.Entity("backend_cine.Models.Seat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<long>("RowId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RowId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("backend_cine.Models.Showtime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("FinishDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME2(3)")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<long>("FormatId")
                        .HasColumnType("bigint");

                    b.Property<long>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<long>("MovieId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME2(3)")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<long>("TheaterId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FormatId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheaterId");

                    b.ToTable("Showtimes");
                });

            modelBuilder.Entity("backend_cine.Models.Theater", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CinemaId")
                        .HasColumnType("bigint");

                    b.Property<string>("TheaterName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("backend_cine.Models.Ticket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("PurchaseId")
                        .HasColumnType("bigint");

                    b.Property<long>("ShowtimeId")
                        .HasColumnType("bigint");

                    b.Property<int>("TicketNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.HasIndex("ShowtimeId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("backend_cine.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CinemaId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME2(3)")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsManager")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("Dni")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CinemaMovie", b =>
                {
                    b.HasOne("backend_cine.Models.Cinema", null)
                        .WithMany()
                        .HasForeignKey("CinemasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FormatMovie", b =>
                {
                    b.HasOne("backend_cine.Models.Format", null)
                        .WithMany()
                        .HasForeignKey("FormatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.HasOne("backend_cine.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LanguageMovie", b =>
                {
                    b.HasOne("backend_cine.Models.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend_cine.Models.Purchase", b =>
                {
                    b.HasOne("backend_cine.Models.User", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend_cine.Models.Row", b =>
                {
                    b.HasOne("backend_cine.Models.Theater", "Theater")
                        .WithMany("Rows")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("backend_cine.Models.Seat", b =>
                {
                    b.HasOne("backend_cine.Models.Row", "Row")
                        .WithMany("Seats")
                        .HasForeignKey("RowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Row");
                });

            modelBuilder.Entity("backend_cine.Models.Showtime", b =>
                {
                    b.HasOne("backend_cine.Models.Format", "Format")
                        .WithMany("Showtimes")
                        .HasForeignKey("FormatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Language", "Language")
                        .WithMany("Showtimes")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Movie", "Movie")
                        .WithMany("Showtimes")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Theater", "Theater")
                        .WithMany("Showtimes")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Format");

                    b.Navigation("Language");

                    b.Navigation("Movie");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("backend_cine.Models.Theater", b =>
                {
                    b.HasOne("backend_cine.Models.Cinema", "Cinema")
                        .WithMany("Theaters")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("backend_cine.Models.Ticket", b =>
                {
                    b.HasOne("backend_cine.Models.Purchase", "Purchase")
                        .WithMany("Tickets")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("backend_cine.Models.Showtime", "Showtime")
                        .WithMany("Tickets")
                        .HasForeignKey("ShowtimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Purchase");

                    b.Navigation("Showtime");
                });

            modelBuilder.Entity("backend_cine.Models.User", b =>
                {
                    b.HasOne("backend_cine.Models.Cinema", "Cinema")
                        .WithMany("Users")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("backend_cine.Models.Cinema", b =>
                {
                    b.Navigation("Theaters");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("backend_cine.Models.Format", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("backend_cine.Models.Language", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("backend_cine.Models.Movie", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("backend_cine.Models.Purchase", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("backend_cine.Models.Row", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("backend_cine.Models.Showtime", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("backend_cine.Models.Theater", b =>
                {
                    b.Navigation("Rows");

                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("backend_cine.Models.User", b =>
                {
                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}
