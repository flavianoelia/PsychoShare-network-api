using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class Profe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPerson",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdPerson",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
