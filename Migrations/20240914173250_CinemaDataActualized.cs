using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class CinemaDataActualized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "gcinema",
                table: "cinema",
                keyColumn: "Id",
                keyValue: 1L,
                column: "address",
                value: "Av. Eva Peron 5856, Rosario, Santa Fe");

            migrationBuilder.UpdateData(
                schema: "gcinema",
                table: "cinema",
                keyColumn: "Id",
                keyValue: 2L,
                column: "address",
                value: "Junin 501, Rosario, Santa Fe");

            migrationBuilder.InsertData(
                schema: "gcinema",
                table: "cinema",
                columns: new[] { "Id", "address", "cinema_name" },
                values: new object[] { 3L, "Zeballos 1341, Rosario, Santa Fe", "CineUTN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "gcinema",
                table: "cinema",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                schema: "gcinema",
                table: "cinema",
                keyColumn: "Id",
                keyValue: 1L,
                column: "address",
                value: "Av. Eva Peron 5856");

            migrationBuilder.UpdateData(
                schema: "gcinema",
                table: "cinema",
                keyColumn: "Id",
                keyValue: 2L,
                column: "address",
                value: "Junin 501");
        }
    }
}
