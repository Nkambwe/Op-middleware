using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Operators.Moddleware.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Themes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ThemeId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ThemeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrimaryColor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SecondaryColor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FontFamily = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nchar(250)", fixedLength: true, maxLength: 250, nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nchar(250)", fixedLength: true, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Themes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Themes_UserId",
                table: "Themes",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Users");
        }
    }
}
