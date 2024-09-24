using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class FixTheaterProblemDeleteDataTheater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Theaters",
                keyColumn: "Id",
                keyValue: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Theaters",
                columns: new[] { "Id", "CinemaId", "TheaterName" },
                values: new object[] { 1L, 1L, "1" });
        }
    }
}
