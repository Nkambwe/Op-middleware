using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Operators.Moddleware.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Themes_users_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTheme_Themes_UserId",
                table: "UserTheme");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTheme_Users_UserId",
                table: "UserTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTheme",
                table: "UserTheme");

            migrationBuilder.DropIndex(
                name: "IX_UserTheme_UserId",
                table: "UserTheme");

            migrationBuilder.RenameTable(
                name: "UserTheme",
                newName: "UserThemes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserThemes",
                table: "UserThemes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ThemeId",
                table: "Users",
                column: "ThemeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserThemes_UserId",
                table: "UserThemes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserThemes_ThemeId",
                table: "Users",
                column: "ThemeId",
                principalTable: "UserThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserThemes_Themes_UserId",
                table: "UserThemes",
                column: "UserId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserThemes_ThemeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserThemes_Themes_UserId",
                table: "UserThemes");

            migrationBuilder.DropIndex(
                name: "IX_Users_ThemeId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserThemes",
                table: "UserThemes");

            migrationBuilder.DropIndex(
                name: "IX_UserThemes_UserId",
                table: "UserThemes");

            migrationBuilder.RenameTable(
                name: "UserThemes",
                newName: "UserTheme");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTheme",
                table: "UserTheme",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_UserId",
                table: "UserTheme",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTheme_Themes_UserId",
                table: "UserTheme",
                column: "UserId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTheme_Users_UserId",
                table: "UserTheme",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
