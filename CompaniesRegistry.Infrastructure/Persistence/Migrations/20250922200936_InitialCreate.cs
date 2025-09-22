using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompaniesRegistry.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Exchange = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Ticker = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Isin = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    WebSite = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.CheckConstraint("CK_Companies_Isin_AlphaPrefix", "\"Isin\" ~ '^[A-Z]{2}'");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Isin",
                table: "Companies",
                column: "Isin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
