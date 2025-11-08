using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class Alter_table_avatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Files_ImageId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ImageId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Pdf_Url",
                table: "Files",
                newName: "Image_Url");

            migrationBuilder.RenameColumn(
                name: "Pdf_IdUser",
                table: "Files",
                newName: "Image_IdUser");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Files",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldMaxLength: 5)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Files",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Followings_FollowedId",
                table: "Followings",
                column: "FollowedId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Persons_FollowedId",
                table: "Followings",
                column: "FollowedId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Persons_UserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Persons_FollowedId",
                table: "Followings");

            migrationBuilder.DropIndex(
                name: "IX_Followings_FollowedId",
                table: "Followings");

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

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Persons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Files",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ImageId",
                table: "Persons",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Files_ImageId",
                table: "Persons",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
