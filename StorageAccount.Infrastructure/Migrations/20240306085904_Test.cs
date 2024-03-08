using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageAccount.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlobFileType",
                table: "FileDetails",
                newName: "FileType");

            migrationBuilder.RenameColumn(
                name: "BlobFileGuid",
                table: "FileDetails",
                newName: "FileGuid");

            migrationBuilder.RenameColumn(
                name: "BlobFileId",
                table: "FileDetails",
                newName: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "FileDetails",
                newName: "BlobFileType");

            migrationBuilder.RenameColumn(
                name: "FileGuid",
                table: "FileDetails",
                newName: "BlobFileGuid");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "FileDetails",
                newName: "BlobFileId");
        }
    }
}
