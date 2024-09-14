using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCinemaAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "gcinema",
                table: "cinema",
                columns: new[] { "Id", "address", "cinema_name" },
                values: new object[,]
                {
                    { 1L, "Av. Eva Peron 5856", "Cinepolis" },
                    { 2L, "Junin 501", "Showcase" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cinema_address",
                schema: "gcinema",
                table: "cinema",
                column: "address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cinema_address",
                schema: "gcinema",
                table: "cinema");

            migrationBuilder.DeleteData(
                schema: "gcinema",
                table: "cinema",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "gcinema",
                table: "cinema",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
