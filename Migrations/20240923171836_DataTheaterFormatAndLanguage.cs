using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class DataTheaterFormatAndLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Formats",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "2D" },
                    { 2L, "3D" },
                    { 3L, "4D" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Spanish" },
                    { 2L, "English" },
                    { 3L, "Chinese" }
                });

            migrationBuilder.InsertData(
                table: "Theaters",
                columns: new[] { "Id", "CinemaId", "TheaterName" },
                values: new object[] { 1L, 1L, "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Formats",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Formats",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Formats",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Theaters",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
