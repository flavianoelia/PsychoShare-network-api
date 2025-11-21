using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Files_to_BaseFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Persons_IdUser",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Files_ImageId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Files_ImgOwnerId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Files_PdfId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "BaseFiles");

            migrationBuilder.RenameIndex(
                name: "IX_Files_IdUser",
                table: "BaseFiles",
                newName: "IX_BaseFiles_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseFiles",
                table: "BaseFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseFiles_Persons_IdUser",
                table: "BaseFiles",
                column: "IdUser",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BaseFiles_ImageId",
                table: "Posts",
                column: "ImageId",
                principalTable: "BaseFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BaseFiles_ImgOwnerId",
                table: "Posts",
                column: "ImgOwnerId",
                principalTable: "BaseFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BaseFiles_PdfId",
                table: "Posts",
                column: "PdfId",
                principalTable: "BaseFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseFiles_Persons_IdUser",
                table: "BaseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BaseFiles_ImageId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BaseFiles_ImgOwnerId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BaseFiles_PdfId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseFiles",
                table: "BaseFiles");

            migrationBuilder.RenameTable(
                name: "BaseFiles",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_BaseFiles_IdUser",
                table: "Files",
                newName: "IX_Files_IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Persons_IdUser",
                table: "Files",
                column: "IdUser",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Files_ImageId",
                table: "Posts",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Files_ImgOwnerId",
                table: "Posts",
                column: "ImgOwnerId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Files_PdfId",
                table: "Posts",
                column: "PdfId",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
