using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_cine.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionNombreDeTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormatMovie_Format_FormatsId",
                schema: "gcinema",
                table: "FormatMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageMovie_Language_LanguagesId",
                schema: "gcinema",
                table: "LanguageMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                schema: "gcinema",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Format",
                schema: "gcinema",
                table: "Format");

            migrationBuilder.RenameTable(
                name: "Language",
                schema: "gcinema",
                newName: "language",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "Format",
                schema: "gcinema",
                newName: "format",
                newSchema: "gcinema");

            migrationBuilder.AddPrimaryKey(
                name: "PK_language",
                schema: "gcinema",
                table: "language",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_format",
                schema: "gcinema",
                table: "format",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormatMovie_format_FormatsId",
                schema: "gcinema",
                table: "FormatMovie",
                column: "FormatsId",
                principalSchema: "gcinema",
                principalTable: "format",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageMovie_language_LanguagesId",
                schema: "gcinema",
                table: "LanguageMovie",
                column: "LanguagesId",
                principalSchema: "gcinema",
                principalTable: "language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormatMovie_format_FormatsId",
                schema: "gcinema",
                table: "FormatMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageMovie_language_LanguagesId",
                schema: "gcinema",
                table: "LanguageMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_language",
                schema: "gcinema",
                table: "language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_format",
                schema: "gcinema",
                table: "format");

            migrationBuilder.RenameTable(
                name: "language",
                schema: "gcinema",
                newName: "Language",
                newSchema: "gcinema");

            migrationBuilder.RenameTable(
                name: "format",
                schema: "gcinema",
                newName: "Format",
                newSchema: "gcinema");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                schema: "gcinema",
                table: "Language",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Format",
                schema: "gcinema",
                table: "Format",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormatMovie_Format_FormatsId",
                schema: "gcinema",
                table: "FormatMovie",
                column: "FormatsId",
                principalSchema: "gcinema",
                principalTable: "Format",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageMovie_Language_LanguagesId",
                schema: "gcinema",
                table: "LanguageMovie",
                column: "LanguagesId",
                principalSchema: "gcinema",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
