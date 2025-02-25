using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class FixedStaticSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsIncome = table.Column<bool>(type: "INTEGER", nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Category", "Date", "Description", "IsIncome", "PaymentMethod" },
                values: new object[,]
                {
                    { 1, 2000m, 4, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salary", true, "Bank Transfer" },
                    { 2, 500m, 0, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Groceries", false, "Credit Card" },
                    { 3, 1200m, 1, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rent", false, "Bank Transfer" },
                    { 4, 150m, 2, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electricity Bill", false, "Debit Card" },
                    { 5, 75m, 3, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie Night", false, "Cash" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
