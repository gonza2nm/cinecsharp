using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class TestCambiarNombreRelacionesMM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemaMovie",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "FormatMovie",
                schema: "gcinema");

            migrationBuilder.CreateTable(
                name: "cinema_movies",
                schema: "gcinema",
                columns: table => new
                {
                    CinemasId = table.Column<long>(type: "bigint", nullable: false),
                    MoviesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cinema_movies", x => new { x.CinemasId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_cinema_movies_cinema_CinemasId",
                        column: x => x.CinemasId,
                        principalSchema: "gcinema",
                        principalTable: "cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cinema_movies_movie_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "gcinema",
                        principalTable: "movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "format_movies",
                schema: "gcinema",
                columns: table => new
                {
                    FormatsId = table.Column<long>(type: "bigint", nullable: false),
                    MoviesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_format_movies", x => new { x.FormatsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_format_movies_format_FormatsId",
                        column: x => x.FormatsId,
                        principalSchema: "gcinema",
                        principalTable: "format",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_format_movies_movie_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "gcinema",
                        principalTable: "movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cinema_movies_MoviesId",
                schema: "gcinema",
                table: "cinema_movies",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_format_movies_MoviesId",
                schema: "gcinema",
                table: "format_movies",
                column: "MoviesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cinema_movies",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "format_movies",
                schema: "gcinema");

            migrationBuilder.CreateTable(
                name: "CinemaMovie",
                schema: "gcinema",
                columns: table => new
                {
                    CinemasId = table.Column<long>(type: "bigint", nullable: false),
                    MoviesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaMovie", x => new { x.CinemasId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CinemaMovie_cinema_CinemasId",
                        column: x => x.CinemasId,
                        principalSchema: "gcinema",
                        principalTable: "cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinemaMovie_movie_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "gcinema",
                        principalTable: "movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormatMovie",
                schema: "gcinema",
                columns: table => new
                {
                    FormatsId = table.Column<long>(type: "bigint", nullable: false),
                    MoviesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatMovie", x => new { x.FormatsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_FormatMovie_format_FormatsId",
                        column: x => x.FormatsId,
                        principalSchema: "gcinema",
                        principalTable: "format",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormatMovie_movie_MoviesId",
                        column: x => x.MoviesId,
                        principalSchema: "gcinema",
                        principalTable: "movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemaMovie_MoviesId",
                schema: "gcinema",
                table: "CinemaMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_FormatMovie_MoviesId",
                schema: "gcinema",
                table: "FormatMovie",
                column: "MoviesId");
        }
    }
}
