using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class ElimineDefaultSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "user",
                schema: "gcinema",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "ticket",
                schema: "gcinema",
                newName: "ticket");

            migrationBuilder.RenameTable(
                name: "theater",
                schema: "gcinema",
                newName: "theater");

            migrationBuilder.RenameTable(
                name: "showtime",
                schema: "gcinema",
                newName: "showtime");

            migrationBuilder.RenameTable(
                name: "row",
                schema: "gcinema",
                newName: "row");

            migrationBuilder.RenameTable(
                name: "purchase",
                schema: "gcinema",
                newName: "purchase");

            migrationBuilder.RenameTable(
                name: "movie",
                schema: "gcinema",
                newName: "movie");

            migrationBuilder.RenameTable(
                name: "LanguageMovie",
                schema: "gcinema",
                newName: "LanguageMovie");

            migrationBuilder.RenameTable(
                name: "language",
                schema: "gcinema",
                newName: "language");

            migrationBuilder.RenameTable(
                name: "FormatMovie",
                schema: "gcinema",
                newName: "FormatMovie");

            migrationBuilder.RenameTable(
                name: "format",
                schema: "gcinema",
                newName: "format");

            migrationBuilder.RenameTable(
                name: "CinemaMovie",
                schema: "gcinema",
                newName: "CinemaMovie");

            migrationBuilder.RenameTable(
                name: "cinema",
                schema: "gcinema",
                newName: "cinema");

            migrationBuilder.RenameTable(
                name: "chair",
                schema: "gcinema",
                newName: "chair");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gcinema");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "user",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "ticket",
                newName: "ticket",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "theater",
                newName: "theater",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "showtime",
                newName: "showtime",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "row",
                newName: "row",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "purchase",
                newName: "purchase",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "movie",
                newName: "movie",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "LanguageMovie",
                newName: "LanguageMovie",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "language",
                newName: "language",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "FormatMovie",
                newName: "FormatMovie",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "format",
                newName: "format",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "CinemaMovie",
                newName: "CinemaMovie",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "cinema",
                newName: "cinema",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "chair",
                newName: "chair",
                newSchema: "gcinema");
        }
    }
}
