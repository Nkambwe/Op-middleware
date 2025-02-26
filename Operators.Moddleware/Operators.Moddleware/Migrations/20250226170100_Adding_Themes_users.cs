using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Operators.Moddleware.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Themes_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Users_UserId",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Themes_UserId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Themes");

            migrationBuilder.CreateTable(
                name: "UserTheme",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ThemeId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTheme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTheme_Themes_UserId",
                        column: x => x.UserId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTheme_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTheme_UserId",
                table: "UserTheme",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTheme");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Themes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.CreateIndex(
                name: "IX_Themes_UserId",
                table: "Themes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Users_UserId",
                table: "Themes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
