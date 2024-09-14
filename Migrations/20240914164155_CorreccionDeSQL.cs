using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionDeSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMovie",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "gcinema");

            migrationBuilder.RenameColumn(
                name: "language_name",
                schema: "gcinema",
                table: "language",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "format_name",
                schema: "gcinema",
                table: "format",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "gcinema",
                table: "user",
                type: "DATETIME2(3)",
                nullable: false,
                defaultValueSql: "SYSDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.CreateTable(
                name: "Purchases",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "DATETIME2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "gcinema",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "theater",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    theater_name = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CinemaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theater", x => x.Id);
                    table.ForeignKey(
                        name: "FK_theater_cinema_CinemaId",
                        column: x => x.CinemaId,
                        principalSchema: "gcinema",
                        principalTable: "cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rows",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    row_no = table.Column<int>(type: "int", nullable: false),
                    total_capacity = table.Column<int>(type: "int", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rows_theater_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "gcinema",
                        principalTable: "theater",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Showtimes",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayAndHourStart = table.Column<DateTime>(type: "DATETIME2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    DayAndHourToFinish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    FormatId = table.Column<long>(type: "bigint", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Showtimes_format_FormatId",
                        column: x => x.FormatId,
                        principalSchema: "gcinema",
                        principalTable: "format",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showtimes_language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "gcinema",
                        principalTable: "language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showtimes_movie_MovieId",
                        column: x => x.MovieId,
                        principalSchema: "gcinema",
                        principalTable: "movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showtimes_theater_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "gcinema",
                        principalTable: "theater",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chairs",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chair_no = table.Column<int>(type: "int", nullable: false),
                    RowId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chairs_Rows_RowId",
                        column: x => x.RowId,
                        principalSchema: "gcinema",
                        principalTable: "Rows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowtimeId = table.Column<long>(type: "bigint", nullable: false),
                    PurchaseId = table.Column<long>(type: "bigint", nullable: false),
                    ticket_no = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalSchema: "gcinema",
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Showtimes_ShowtimeId",
                        column: x => x.ShowtimeId,
                        principalSchema: "gcinema",
                        principalTable: "Showtimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_RowId",
                schema: "gcinema",
                table: "Chairs",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId",
                schema: "gcinema",
                table: "Purchases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_TheaterId",
                schema: "gcinema",
                table: "Rows",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_FormatId",
                schema: "gcinema",
                table: "Showtimes",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_LanguageId",
                schema: "gcinema",
                table: "Showtimes",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_MovieId",
                schema: "gcinema",
                table: "Showtimes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_TheaterId",
                schema: "gcinema",
                table: "Showtimes",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_theater_CinemaId",
                schema: "gcinema",
                table: "theater",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PurchaseId",
                schema: "gcinema",
                table: "Tickets",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShowtimeId",
                schema: "gcinema",
                table: "Tickets",
                column: "ShowtimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chairs",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "Rows",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "Purchases",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "Showtimes",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "theater",
                schema: "gcinema");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "gcinema",
                table: "language",
                newName: "language_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "gcinema",
                table: "format",
                newName: "format_name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "gcinema",
                table: "user",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2(3)",
                oldDefaultValueSql: "SYSDATETIME()");

            migrationBuilder.CreateTable(
                name: "genre",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                schema: "gcinema",
                columns: table => new
                {
                    GenresId = table.Column<long>(type: "bigint", nullable: false),
                    MoviesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_genre_GenresId",
                        column: x => x.GenresId,
                        principalSchema: "gcinema",
                        principalTable: "genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_movie_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "gcinema",
                        principalTable: "movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                schema: "gcinema",
                table: "GenreMovie",
                column: "MoviesId");
        }
    }
}
