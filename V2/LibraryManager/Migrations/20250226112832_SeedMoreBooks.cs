using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManager.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CoverImageUrl", "Description", "Genre", "ISBN", "IsAvailable", "PublicationYear", "Title", "UserId" },
                values: new object[,]
                {
                    { 3, "George Orwell", "https://example.com/1984.jpg", "A dystopian social science fiction novel and cautionary tale.", "Dystopian", "9780451524935", true, 1949, "1984", "admin-user-id" },
                    { 4, "Herman Melville", "https://example.com/mobydick.jpg", "A narrative of the adventures of the whaling ship Pequod.", "Adventure", "9781503280786", true, 1851, "Moby-Dick", "admin-user-id" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
