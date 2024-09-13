using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class FormatLanguageMovieGenreMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_cinemas_cinema_id",
                schema: "gcinema",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "gcinema",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cinemas",
                schema: "gcinema",
                table: "cinemas");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "gcinema",
                newName: "user",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "cinemas",
                schema: "gcinema",
                newName: "cinema",
                newSchema: "gcinema");

            migrationBuilder.RenameIndex(
                name: "IX_users_Dni",
                schema: "gcinema",
                table: "user",
                newName: "IX_user_Dni");

            migrationBuilder.RenameIndex(
                name: "IX_users_cinema_id",
                schema: "gcinema",
                table: "user",
                newName: "IX_user_cinema_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                schema: "gcinema",
                table: "user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cinema",
                schema: "gcinema",
                table: "cinema",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Format",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    format_name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Format", x => x.Id);
                });

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
                name: "Language",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    language_name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movie",
                schema: "gcinema",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.Id);
                });

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
                        name: "FK_FormatMovie_Format_FormatsId",
                        column: x => x.FormatsId,
                        principalSchema: "gcinema",
                        principalTable: "Format",
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

            migrationBuilder.CreateTable(
                name: "LanguageMovie",
                schema: "gcinema",
                columns: table => new
                {
                    LanguagesId = table.Column<long>(type: "bigint", nullable: false),
                    MoviesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageMovie", x => new { x.LanguagesId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_LanguageMovie_Language_LanguagesId",
                        column: x => x.LanguagesId,
                        principalSchema: "gcinema",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageMovie_movie_MoviesId",
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

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                schema: "gcinema",
                table: "GenreMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageMovie_MoviesId",
                schema: "gcinema",
                table: "LanguageMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_cinema_cinema_id",
                schema: "gcinema",
                table: "user",
                column: "cinema_id",
                principalSchema: "gcinema",
                principalTable: "cinema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_cinema_cinema_id",
                schema: "gcinema",
                table: "user");

            migrationBuilder.DropTable(
                name: "CinemaMovie",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "FormatMovie",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "GenreMovie",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "LanguageMovie",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "Format",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "genre",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "gcinema");

            migrationBuilder.DropTable(
                name: "movie",
                schema: "gcinema");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                schema: "gcinema",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cinema",
                schema: "gcinema",
                table: "cinema");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "gcinema",
                newName: "users",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "cinema",
                schema: "gcinema",
                newName: "cinemas",
                newSchema: "gcinema");

            migrationBuilder.RenameIndex(
                name: "IX_user_Dni",
                schema: "gcinema",
                table: "users",
                newName: "IX_users_Dni");

            migrationBuilder.RenameIndex(
                name: "IX_user_cinema_id",
                schema: "gcinema",
                table: "users",
                newName: "IX_users_cinema_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "gcinema",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cinemas",
                schema: "gcinema",
                table: "cinemas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_cinemas_cinema_id",
                schema: "gcinema",
                table: "users",
                column: "cinema_id",
                principalSchema: "gcinema",
                principalTable: "cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
