using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Comentado porque el índice ya no existe o fue eliminado previamente
            // migrationBuilder.DropIndex(
            //     name: "IX_BaseFiles_IdUser",
            //     table: "BaseFiles");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseFiles_IdUser",
                table: "BaseFiles",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_BaseFiles_Pdf_IdUser",
                table: "BaseFiles",
                column: "Pdf_IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseFiles_Persons_Pdf_IdUser",
                table: "BaseFiles",
                column: "Pdf_IdUser",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Persons_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseFiles_Persons_Pdf_IdUser",
                table: "BaseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Persons_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_BaseFiles_IdUser",
                table: "BaseFiles");

            migrationBuilder.DropIndex(
                name: "IX_BaseFiles_Pdf_IdUser",
                table: "BaseFiles");

            migrationBuilder.CreateIndex(
                name: "IX_BaseFiles_IdUser",
                table: "BaseFiles",
                column: "IdUser",
                unique: true);
        }
    }
}
