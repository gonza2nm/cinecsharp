using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class TestIdGeneral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayAndHourToFinish",
                table: "Showtimes");

            migrationBuilder.RenameColumn(
                name: "DayAndHourStart",
                table: "Showtimes",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "Showtimes",
                type: "DATETIME2(3)",
                nullable: false,
                defaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Movies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenresId = table.Column<long>(type: "bigint", nullable: false),
                    MoviesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                column: "MoviesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Showtimes",
                newName: "DayAndHourStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayAndHourToFinish",
                table: "Showtimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
