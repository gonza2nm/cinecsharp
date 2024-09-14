using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionDeEntitidadesMAYUSCULAS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Rows_RowId",
                schema: "gcinema",
                table: "Chairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_user_UserId",
                schema: "gcinema",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_theater_TheaterId",
                schema: "gcinema",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_format_FormatId",
                schema: "gcinema",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_language_LanguageId",
                schema: "gcinema",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_movie_MovieId",
                schema: "gcinema",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Showtimes_theater_TheaterId",
                schema: "gcinema",
                table: "Showtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Purchases_PurchaseId",
                schema: "gcinema",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                schema: "gcinema",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                schema: "gcinema",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Showtimes",
                schema: "gcinema",
                table: "Showtimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rows",
                schema: "gcinema",
                table: "Rows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                schema: "gcinema",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chairs",
                schema: "gcinema",
                table: "Chairs");

            migrationBuilder.RenameTable(
                name: "Tickets",
                schema: "gcinema",
                newName: "ticket",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "Showtimes",
                schema: "gcinema",
                newName: "showtime",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "Rows",
                schema: "gcinema",
                newName: "row",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "Purchases",
                schema: "gcinema",
                newName: "purchase",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "Chairs",
                schema: "gcinema",
                newName: "chair",
                newSchema: "gcinema");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ShowtimeId",
                schema: "gcinema",
                table: "ticket",
                newName: "IX_ticket_ShowtimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_PurchaseId",
                schema: "gcinema",
                table: "ticket",
                newName: "IX_ticket_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_TheaterId",
                schema: "gcinema",
                table: "showtime",
                newName: "IX_showtime_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_MovieId",
                schema: "gcinema",
                table: "showtime",
                newName: "IX_showtime_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_LanguageId",
                schema: "gcinema",
                table: "showtime",
                newName: "IX_showtime_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Showtimes_FormatId",
                schema: "gcinema",
                table: "showtime",
                newName: "IX_showtime_FormatId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_TheaterId",
                schema: "gcinema",
                table: "row",
                newName: "IX_row_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_UserId",
                schema: "gcinema",
                table: "purchase",
                newName: "IX_purchase_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chairs_RowId",
                schema: "gcinema",
                table: "chair",
                newName: "IX_chair_RowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ticket",
                schema: "gcinema",
                table: "ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_showtime",
                schema: "gcinema",
                table: "showtime",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_row",
                schema: "gcinema",
                table: "row",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchase",
                schema: "gcinema",
                table: "purchase",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chair",
                schema: "gcinema",
                table: "chair",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chair_row_RowId",
                schema: "gcinema",
                table: "chair",
                column: "RowId",
                principalSchema: "gcinema",
                principalTable: "row",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_user_UserId",
                schema: "gcinema",
                table: "purchase",
                column: "UserId",
                principalSchema: "gcinema",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_theater_TheaterId",
                schema: "gcinema",
                table: "row",
                column: "TheaterId",
                principalSchema: "gcinema",
                principalTable: "theater",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_format_FormatId",
                schema: "gcinema",
                table: "showtime",
                column: "FormatId",
                principalSchema: "gcinema",
                principalTable: "format",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_language_LanguageId",
                schema: "gcinema",
                table: "showtime",
                column: "LanguageId",
                principalSchema: "gcinema",
                principalTable: "language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_movie_MovieId",
                schema: "gcinema",
                table: "showtime",
                column: "MovieId",
                principalSchema: "gcinema",
                principalTable: "movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_showtime_theater_TheaterId",
                schema: "gcinema",
                table: "showtime",
                column: "TheaterId",
                principalSchema: "gcinema",
                principalTable: "theater",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_purchase_PurchaseId",
                schema: "gcinema",
                table: "ticket",
                column: "PurchaseId",
                principalSchema: "gcinema",
                principalTable: "purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_showtime_ShowtimeId",
                schema: "gcinema",
                table: "ticket",
                column: "ShowtimeId",
                principalSchema: "gcinema",
                principalTable: "showtime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chair_row_RowId",
                schema: "gcinema",
                table: "chair");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_user_UserId",
                schema: "gcinema",
                table: "purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_row_theater_TheaterId",
                schema: "gcinema",
                table: "row");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_format_FormatId",
                schema: "gcinema",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_language_LanguageId",
                schema: "gcinema",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_movie_MovieId",
                schema: "gcinema",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_showtime_theater_TheaterId",
                schema: "gcinema",
                table: "showtime");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_purchase_PurchaseId",
                schema: "gcinema",
                table: "ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_showtime_ShowtimeId",
                schema: "gcinema",
                table: "ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ticket",
                schema: "gcinema",
                table: "ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_showtime",
                schema: "gcinema",
                table: "showtime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_row",
                schema: "gcinema",
                table: "row");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchase",
                schema: "gcinema",
                table: "purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_chair",
                schema: "gcinema",
                table: "chair");

            migrationBuilder.RenameTable(
                name: "ticket",
                schema: "gcinema",
                newName: "Tickets",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "showtime",
                schema: "gcinema",
                newName: "Showtimes",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "row",
                schema: "gcinema",
                newName: "Rows",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "purchase",
                schema: "gcinema",
                newName: "Purchases",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "chair",
                schema: "gcinema",
                newName: "Chairs",
                newSchema: "gcinema");

            migrationBuilder.RenameIndex(
                name: "IX_ticket_ShowtimeId",
                schema: "gcinema",
                table: "Tickets",
                newName: "IX_Tickets_ShowtimeId");

            migrationBuilder.RenameIndex(
                name: "IX_ticket_PurchaseId",
                schema: "gcinema",
                table: "Tickets",
                newName: "IX_Tickets_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_TheaterId",
                schema: "gcinema",
                table: "Showtimes",
                newName: "IX_Showtimes_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_MovieId",
                schema: "gcinema",
                table: "Showtimes",
                newName: "IX_Showtimes_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_LanguageId",
                schema: "gcinema",
                table: "Showtimes",
                newName: "IX_Showtimes_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_showtime_FormatId",
                schema: "gcinema",
                table: "Showtimes",
                newName: "IX_Showtimes_FormatId");

            migrationBuilder.RenameIndex(
                name: "IX_row_TheaterId",
                schema: "gcinema",
                table: "Rows",
                newName: "IX_Rows_TheaterId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_UserId",
                schema: "gcinema",
                table: "Purchases",
                newName: "IX_Purchases_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_chair_RowId",
                schema: "gcinema",
                table: "Chairs",
                newName: "IX_Chairs_RowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                schema: "gcinema",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Showtimes",
                schema: "gcinema",
                table: "Showtimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rows",
                schema: "gcinema",
                table: "Rows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                schema: "gcinema",
                table: "Purchases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chairs",
                schema: "gcinema",
                table: "Chairs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Rows_RowId",
                schema: "gcinema",
                table: "Chairs",
                column: "RowId",
                principalSchema: "gcinema",
                principalTable: "Rows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_user_UserId",
                schema: "gcinema",
                table: "Purchases",
                column: "UserId",
                principalSchema: "gcinema",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_theater_TheaterId",
                schema: "gcinema",
                table: "Rows",
                column: "TheaterId",
                principalSchema: "gcinema",
                principalTable: "theater",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_format_FormatId",
                schema: "gcinema",
                table: "Showtimes",
                column: "FormatId",
                principalSchema: "gcinema",
                principalTable: "format",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_language_LanguageId",
                schema: "gcinema",
                table: "Showtimes",
                column: "LanguageId",
                principalSchema: "gcinema",
                principalTable: "language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_movie_MovieId",
                schema: "gcinema",
                table: "Showtimes",
                column: "MovieId",
                principalSchema: "gcinema",
                principalTable: "movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Showtimes_theater_TheaterId",
                schema: "gcinema",
                table: "Showtimes",
                column: "TheaterId",
                principalSchema: "gcinema",
                principalTable: "theater",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Purchases_PurchaseId",
                schema: "gcinema",
                table: "Tickets",
                column: "PurchaseId",
                principalSchema: "gcinema",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Showtimes_ShowtimeId",
                schema: "gcinema",
                table: "Tickets",
                column: "ShowtimeId",
                principalSchema: "gcinema",
                principalTable: "Showtimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
