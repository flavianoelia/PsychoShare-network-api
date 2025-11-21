using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dao_library.Migrations
{
    /// <inheritdoc />
    public partial class Post_modification_File_add_to_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
               
            migrationBuilder.DropForeignKey( 
                name: "FK_Persons_Images_ImageId",
                table: "Persons"); 

            migrationBuilder.DropForeignKey(
                 name: "FK_Posts_Images_ImageId",
                 table: "Posts");

           migrationBuilder.DropForeignKey(
               name: "FK_Posts_Images_ImgOwnerId",
               table: "Posts");

           migrationBuilder.DropForeignKey(
              name: "FK_Posts_Pdfs_PdfId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pdfs",
                table: "Pdfs");

            migrationBuilder.RenameTable(
                name: "Pdfs",
                newName: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Files",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Files",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "IdUser",
                table: "Files",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Files",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "Files",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "Pdf_IdUser",
                table: "Files",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pdf_Url",
                table: "Files",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Files_ImageId",
                table: "Persons",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
             name: "FK_Persons_Files_ImageId",
             table: "Persons");

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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Pdf_IdUser",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Pdf_Url",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Pdfs");

            migrationBuilder.UpdateData(
                table: "Pdfs",
                keyColumn: "Url",
                keyValue: null,
                column: "Url",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Pdfs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Pdfs",
                keyColumn: "Title",
                keyValue: null,
                column: "Title",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Pdfs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "IdUser",
                table: "Pdfs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pdfs",
                table: "Pdfs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    ImageType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Images_ImageId",
                table: "Persons",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_ImageId",
                table: "Posts",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_ImgOwnerId",
                table: "Posts",
                column: "ImgOwnerId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Pdfs_PdfId",
                table: "Posts",
                column: "PdfId",
                principalTable: "Pdfs",
                principalColumn: "Id");
        }
    }
}
