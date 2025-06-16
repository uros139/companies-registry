using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompaniesRegistry.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompaniesSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Exchange", "Isin", "Name", "Ticker", "WebSite" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "NASDAQ", "US1234567890", "Contoso Ltd.", "CTSO", "https://contoso.com" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "NYSE", "US0987654321", "Fabrikam Inc.", "FBKM", "https://fabrikam.com" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "LSE", "GB1234567890", "Northwind Traders", "NWT", "https://northwindtraders.com" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "TSX", "CA1234567890", "Adventure Works", "ADVW", "https://adventure-works.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));
        }
    }
}
