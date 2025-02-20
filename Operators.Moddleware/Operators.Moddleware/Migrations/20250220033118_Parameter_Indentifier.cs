using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Operators.Moddleware.Migrations
{
    /// <inheritdoc />
    public partial class Parameter_Indentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Parameters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Parameters");
        }
    }
}
