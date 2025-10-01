using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Post_correccion_sintaxis_campos_requeridos_imageOwner_opcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "resume",
                table: "Posts",
                newName: "Resume");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Posts",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "authorship",
                table: "Posts",
                newName: "Authorship");

            migrationBuilder.RenameColumn(
                name: "imgOwner",
                table: "Posts",
                newName: "LastnameOwner");

            migrationBuilder.AddColumn<long>(
                name: "ImgOwnerId",
                table: "Posts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImgOwnerId",
                table: "Posts",
                column: "ImgOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_ImgOwnerId",
                table: "Posts",
                column: "ImgOwnerId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Images_ImgOwnerId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ImgOwnerId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImgOwnerId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Resume",
                table: "Posts",
                newName: "resume");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Posts",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Authorship",
                table: "Posts",
                newName: "authorship");

            migrationBuilder.RenameColumn(
                name: "LastnameOwner",
                table: "Posts",
                newName: "imgOwner");
        }
    }
}
