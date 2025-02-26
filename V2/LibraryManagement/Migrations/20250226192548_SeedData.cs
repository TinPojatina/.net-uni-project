using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Biography", "BirthDate", "FirstName", "LastName", "Nationality" },
                values: new object[,]
                {
                    { 1, "British writer famous for dystopian novels.", new DateTime(1903, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "George", "Orwell", "British" },
                    { 2, "British author, best known for the Harry Potter series.", new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.K.", "Rowling", "British" },
                    { 3, "English writer and professor, famous for The Lord of the Rings.", new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "J.R.R.", "Tolkien", "British" },
                    { 4, "English writer known for detective novels.", new DateTime(1890, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agatha", "Christie", "British" },
                    { 5, "American writer and humorist.", new DateTime(1835, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark", "Twain", "American" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "Genre", "ISBN", "IsAvailable", "Price", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Dystopian", "978-0451524935", true, 15.99m, 1949, "1984" },
                    { 2, 1, "Political satire", "978-0451526342", true, 9.99m, 1945, "Animal Farm" },
                    { 3, 2, "Fantasy", "978-0439708180", true, 29.99m, 1997, "Harry Potter and the Sorcerer’s Stone" },
                    { 4, 2, "Fantasy", "978-0439064873", true, 24.99m, 1998, "Harry Potter and the Chamber of Secrets" },
                    { 5, 3, "Fantasy", "978-0345339683", true, 19.99m, 1937, "The Hobbit" },
                    { 6, 3, "Fantasy", "978-0261102385", true, 49.99m, 1954, "The Lord of the Rings" },
                    { 7, 4, "Mystery", "978-0062693662", true, 14.99m, 1934, "Murder on the Orient Express" },
                    { 8, 5, "Adventure", "978-0486280615", true, 12.99m, 1885, "The Adventures of Huckleberry Finn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 5);
        }
    }
}
