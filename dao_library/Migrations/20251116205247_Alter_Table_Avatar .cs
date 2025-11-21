using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Table_Avatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Persons_UserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Image_Url",
                table: "Files",
                newName: "Pdf_Url");

            migrationBuilder.RenameColumn(
                name: "Image_IdUser",
                table: "Files",
                newName: "Pdf_IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Files_IdUser",
                table: "Files",
                column: "IdUser",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Persons_IdUser",
                table: "Files",
                column: "IdUser",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Persons_IdUser",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_IdUser",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Pdf_Url",
                table: "Files",
                newName: "Image_Url");

            migrationBuilder.RenameColumn(
                name: "Pdf_IdUser",
                table: "Files",
                newName: "Image_IdUser");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Files",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Persons_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
