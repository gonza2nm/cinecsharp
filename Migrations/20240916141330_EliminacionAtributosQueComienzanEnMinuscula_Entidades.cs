using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionAtributosQueComienzanEnMinuscula_Entidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chair_row_RowId",
                table: "chair");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemaMovie_cinema_CinemasId",
                table: "CinemaMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemaMovie_movie_MoviesId",
                table: "CinemaMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_FormatMovie_format_FormatsId",
                table: "FormatMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_FormatMovie_movie_MoviesId",
                table: "FormatMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageMovie_language_LanguagesId",
                table: "LanguageMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageMovie_movie_MoviesId",
                table: "LanguageMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_user_UserId",
                table: "purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_row_theater_TheaterId",
                table: "row");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_format_FormatId",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_language_LanguageId",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_movie_MovieId",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_theater_TheaterId",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_theater_cinema_CinemaId",
                table: "theater");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_purchase_PurchaseId",
                table: "ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_showtime_ShowtimeId",
                table: "ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_user_cinema_cinema_id",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ticket",
                table: "ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_theater",
                table: "theater");

            migrationBuilder.DropPrimaryKey(
                name: "PK_showtime",
                table: "showtime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_row",
                table: "row");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchase",
                table: "purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_movie",
                table: "movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_language",
                table: "language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_format",
                table: "format");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cinema",
                table: "cinema");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chair",
                table: "chair");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "theater",
                newName: "Theaters");

            migrationBuilder.RenameTable(
                name: "showtime",
                newName: "Showtimes");

            migrationBuilder.RenameTable(
                name: "row",
                newName: "Rows");

            migrationBuilder.RenameTable(
                name: "purchase",
                newName: "Purchases");

            migrationBuilder.RenameTable(
                name: "movie",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "language",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "format",
                newName: "Formats");

            migrationBuilder.RenameTable(
                name: "cinema",
                newName: "Cinemas");

            migrationBuilder.RenameTable(
                name: "chair",
                newName: "Chairs");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dni",
                table: "Users",
                newName: "Dni");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_manager",
                table: "Users",
                newName: "IsManager");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "Users",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "cinema_id",
                table: "Users",
                newName: "CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_user_email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_user_dni",
                table: "Users",
                newName: "IX_Users_Dni");

            migrationBuilder.RenameIndex(
                name: "IX_user_cinema_id",
                table: "Users",
                newName: "IX_Users_CinemaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tickets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ticket_no",
                table: "Tickets",
                newName: "TicketNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ticket_ShowtimeId",
                table: "Tickets",
                newName: "IX_Tickets_ShowtimeId");

            migrationBuilder.RenameIndex(
                name: "IX_ticket_PurchaseId",
                table: "Tickets",
                newName: "IX_Tickets_PurchaseId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Theaters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "theater_name",
                table: "Theaters",
                newName: "TheaterName");

            migrationBuilder.RenameIndex(
                name: "IX_theater_CinemaId",
                table: "Theaters",
                newName: "IX_Theaters_CinemaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Showtimes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "day_hour_start",
                table: "Showtimes",
                newName: "DayAndHourStart");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_TheaterId",
                table: "Showtimes",
                newName: "IX_Showtimes_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_MovieId",
                table: "Showtimes",
                newName: "IX_Showtimes_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_LanguageId",
                table: "Showtimes",
                newName: "IX_Showtimes_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_FormatId",
                table: "Showtimes",
                newName: "IX_Showtimes_FormatId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Rows",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "total_capacity",
                table: "Rows",
                newName: "TotalCapacity");

            migrationBuilder.RenameColumn(
                name: "row_no",
                table: "Rows",
                newName: "RowNumber");

            migrationBuilder.RenameIndex(
                name: "IX_row_TheaterId",
                table: "Rows",
                newName: "IX_Rows_TheaterId");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "Purchases",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Purchases",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "purchase_date",
                table: "Purchases",
                newName: "PurchaseDate");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_UserId",
                table: "Purchases",
                newName: "IX_Purchases_UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Movies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Movies",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Movies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Languages",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Languages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_language_name",
                table: "Languages",
                newName: "IX_Languages_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Formats",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Formats",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_format_name",
                table: "Formats",
                newName: "IX_Formats_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Cinemas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Cinemas",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Cinemas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_cinema_address",
                table: "Cinemas",
                newName: "IX_Cinemas_Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Chairs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "chair_no",
                table: "Chairs",
                newName: "ChairNumber");

            migrationBuilder.RenameIndex(
                name: "IX_chair_RowId",
                table: "Chairs",
                newName: "IX_Chairs_RowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Theaters",
                table: "Theaters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rows",
                table: "Rows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Formats",
                table: "Formats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cinemas",
                table: "Cinemas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chairs",
                table: "Chairs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Rows_RowId",
                table: "Chairs",
                column: "RowId",
                principalTable: "Rows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaMovie_Cinemas_CinemasId",
                table: "CinemaMovie",
                column: "CinemasId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaMovie_Movies_MoviesId",
                table: "CinemaMovie",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormatMovie_Formats_FormatsId",
                table: "FormatMovie",
                column: "FormatsId",
                principalTable: "Formats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormatMovie_Movies_MoviesId",
                table: "FormatMovie",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageMovie_Languages_LanguagesId",
                table: "LanguageMovie",
                column: "LanguagesId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageMovie_Movies_MoviesId",
                table: "LanguageMovie",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_Theaters_TheaterId",
                table: "Rows",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Formats_FormatId",
                table: "Showtimes",
                column: "FormatId",
                principalTable: "Formats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Languages_LanguageId",
                table: "Showtimes",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_Theaters_TheaterId",
                table: "Showtimes",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_Cinemas_CinemaId",
                table: "Theaters",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Purchases_PurchaseId",
                table: "Tickets",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets",
                column: "ShowtimeId",
                principalTable: "Showtimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cinemas_CinemaId",
                table: "Users",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Rows_RowId",
                table: "Chairs");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemaMovie_Cinemas_CinemasId",
                table: "CinemaMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemaMovie_Movies_MoviesId",
                table: "CinemaMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_FormatMovie_Formats_FormatsId",
                table: "FormatMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_FormatMovie_Movies_MoviesId",
                table: "FormatMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageMovie_Languages_LanguagesId",
                table: "LanguageMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageMovie_Movies_MoviesId",
                table: "LanguageMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_UserId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_Theaters_TheaterId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Formats_FormatId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Languages_LanguageId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Movies_MovieId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_Theaters_TheaterId",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_Cinemas_CinemaId",
                table: "Theaters");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Purchases_PurchaseId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cinemas_CinemaId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Theaters",
                table: "Theaters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtimes",
                table: "Showtimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rows",
                table: "Rows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Formats",
                table: "Formats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cinemas",
                table: "Cinemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chairs",
                table: "Chairs");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "ticket");

            migrationBuilder.RenameTable(
                name: "Theaters",
                newName: "theater");

            migrationBuilder.RenameTable(
                name: "Showtimes",
                newName: "showtime");

            migrationBuilder.RenameTable(
                name: "Rows",
                newName: "row");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "purchase");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "movie");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "language");

            migrationBuilder.RenameTable(
                name: "Formats",
                newName: "format");

            migrationBuilder.RenameTable(
                name: "Cinemas",
                newName: "cinema");

            migrationBuilder.RenameTable(
                name: "Chairs",
                newName: "chair");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "user",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "user",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Dni",
                table: "user",
                newName: "dni");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsManager",
                table: "user",
                newName: "is_manager");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "user",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CinemaId",
                table: "user",
                newName: "cinema_id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "user",
                newName: "IX_user_email");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Dni",
                table: "user",
                newName: "IX_user_dni");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CinemaId",
                table: "user",
                newName: "IX_user_cinema_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ticket",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TicketNumber",
                table: "ticket",
                newName: "ticket_no");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ShowtimeId",
                table: "ticket",
                newName: "IX_ticket_ShowtimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PurchaseId",
                table: "ticket",
                newName: "IX_ticket_PurchaseId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "theater",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TheaterName",
                table: "theater",
                newName: "theater_name");

            migrationBuilder.RenameIndex(
                name: "IX_Theaters_CinemaId",
                table: "theater",
                newName: "IX_theater_CinemaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "showtime",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DayAndHourStart",
                table: "showtime",
                newName: "day_hour_start");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_TheaterId",
                table: "showtime",
                newName: "IX_showtime_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_MovieId",
                table: "showtime",
                newName: "IX_showtime_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_LanguageId",
                table: "showtime",
                newName: "IX_showtime_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_FormatId",
                table: "showtime",
                newName: "IX_showtime_FormatId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "row",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TotalCapacity",
                table: "row",
                newName: "total_capacity");

            migrationBuilder.RenameColumn(
                name: "RowNumber",
                table: "row",
                newName: "row_no");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_TheaterId",
                table: "row",
                newName: "IX_row_TheaterId");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "purchase",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "purchase",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "purchase",
                newName: "purchase_date");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_UserId",
                table: "purchase",
                newName: "IX_purchase_UserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "movie",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "movie",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "movie",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "language",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "language",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_Name",
                table: "language",
                newName: "IX_language_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "format",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "format",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Formats_Name",
                table: "format",
                newName: "IX_format_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "cinema",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "cinema",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cinema",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Cinemas_Address",
                table: "cinema",
                newName: "IX_cinema_address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "chair",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ChairNumber",
                table: "chair",
                newName: "chair_no");

            migrationBuilder.RenameIndex(
                name: "IX_Chairs_RowId",
                table: "chair",
                newName: "IX_chair_RowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ticket",
                table: "ticket",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_theater",
                table: "theater",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_showtime",
                table: "showtime",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_row",
                table: "row",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchase",
                table: "purchase",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_movie",
                table: "movie",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_language",
                table: "language",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_format",
                table: "format",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cinema",
                table: "cinema",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chair",
                table: "chair",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_chair_row_RowId",
                table: "chair",
                column: "RowId",
                principalTable: "row",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaMovie_cinema_CinemasId",
                table: "CinemaMovie",
                column: "CinemasId",
                principalTable: "cinema",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaMovie_movie_MoviesId",
                table: "CinemaMovie",
                column: "MoviesId",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormatMovie_format_FormatsId",
                table: "FormatMovie",
                column: "FormatsId",
                principalTable: "format",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormatMovie_movie_MoviesId",
                table: "FormatMovie",
                column: "MoviesId",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageMovie_language_LanguagesId",
                table: "LanguageMovie",
                column: "LanguagesId",
                principalTable: "language",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageMovie_movie_MoviesId",
                table: "LanguageMovie",
                column: "MoviesId",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_user_UserId",
                table: "purchase",
                column: "UserId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_theater_TheaterId",
                table: "row",
                column: "TheaterId",
                principalTable: "theater",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_format_FormatId",
                table: "showtime",
                column: "FormatId",
                principalTable: "format",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_language_LanguageId",
                table: "showtime",
                column: "LanguageId",
                principalTable: "language",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_movie_MovieId",
                table: "showtime",
                column: "MovieId",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_theater_TheaterId",
                table: "showtime",
                column: "TheaterId",
                principalTable: "theater",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theater_cinema_CinemaId",
                table: "theater",
                column: "CinemaId",
                principalTable: "cinema",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_purchase_PurchaseId",
                table: "ticket",
                column: "PurchaseId",
                principalTable: "purchase",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_showtime_ShowtimeId",
                table: "ticket",
                column: "ShowtimeId",
                principalTable: "showtime",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_cinema_cinema_id",
                table: "user",
                column: "cinema_id",
                principalTable: "cinema",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
