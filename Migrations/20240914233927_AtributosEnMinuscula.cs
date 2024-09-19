using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class AtributosEnMinuscula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                schema: "gcinema",
                table: "user",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "gcinema",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "gcinema",
                table: "user",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "gcinema",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Dni",
                schema: "gcinema",
                table: "user",
                newName: "dni");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsManager",
                schema: "gcinema",
                table: "user",
                newName: "is_manager");

            migrationBuilder.RenameColumn(
                name: "Created",
                schema: "gcinema",
                table: "user",
                newName: "created_date");

            migrationBuilder.RenameIndex(
                name: "IX_user_Dni",
                schema: "gcinema",
                table: "user",
                newName: "IX_user_dni");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "ticket",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "theater",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "showtime",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DayAndHourStart",
                schema: "gcinema",
                table: "showtime",
                newName: "day_hour_start");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "row",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "purchase",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                schema: "gcinema",
                table: "purchase",
                newName: "purchase_date");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "gcinema",
                table: "movie",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "gcinema",
                table: "movie",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "movie",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "gcinema",
                table: "language",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "language",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "gcinema",
                table: "format",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "format",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "cinema",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "cinema_name",
                schema: "gcinema",
                table: "cinema",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "gcinema",
                table: "chair",
                newName: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                schema: "gcinema",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_language_name",
                schema: "gcinema",
                table: "language",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_format_name",
                schema: "gcinema",
                table: "format",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_email",
                schema: "gcinema",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_language_name",
                schema: "gcinema",
                table: "language");

            migrationBuilder.DropIndex(
                name: "IX_format_name",
                schema: "gcinema",
                table: "format");

            migrationBuilder.RenameColumn(
                name: "surname",
                schema: "gcinema",
                table: "user",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "password",
                schema: "gcinema",
                table: "user",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "gcinema",
                table: "user",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "gcinema",
                table: "user",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dni",
                schema: "gcinema",
                table: "user",
                newName: "Dni");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "user",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_manager",
                schema: "gcinema",
                table: "user",
                newName: "IsManager");

            migrationBuilder.RenameColumn(
                name: "created_date",
                schema: "gcinema",
                table: "user",
                newName: "Created");

            migrationBuilder.RenameIndex(
                name: "IX_user_dni",
                schema: "gcinema",
                table: "user",
                newName: "IX_user_Dni");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "ticket",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "theater",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "showtime",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "day_hour_start",
                schema: "gcinema",
                table: "showtime",
                newName: "DayAndHourStart");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "row",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "purchase",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "purchase_date",
                schema: "gcinema",
                table: "purchase",
                newName: "PurchaseDate");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "gcinema",
                table: "movie",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "gcinema",
                table: "movie",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "movie",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "gcinema",
                table: "language",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "language",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "gcinema",
                table: "format",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "format",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "cinema",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "gcinema",
                table: "cinema",
                newName: "cinema_name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "gcinema",
                table: "chair",
                newName: "Id");
        }
    }
}
