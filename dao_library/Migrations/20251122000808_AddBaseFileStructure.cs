using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseFileStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BaseFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Pdf_Name",
                table: "BaseFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "BaseFiles");

            migrationBuilder.DropColumn(
                name: "Pdf_Name",
                table: "BaseFiles");
        }
    }
}
