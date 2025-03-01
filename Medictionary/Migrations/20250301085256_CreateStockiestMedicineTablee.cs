using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class CreateStockiestMedicineTablee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e5aca14-84ca-406e-a808-4d444585d883");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ff4301c-994c-4a95-8a6b-7da2ca1449bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "584b0c55-b7c2-4714-b8de-a32ed9119610");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39f6b538-918e-4f97-b780-9f9168928f63", null, "Stockiest", "STOCKIEST" },
                    { "8040c6bd-8016-4355-878b-c14046eee10c", null, "User", "USER" },
                    { "ab54aacd-05a4-4142-86e7-714a9e0aa3ca", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39f6b538-918e-4f97-b780-9f9168928f63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8040c6bd-8016-4355-878b-c14046eee10c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab54aacd-05a4-4142-86e7-714a9e0aa3ca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e5aca14-84ca-406e-a808-4d444585d883", null, "Admin", "ADMIN" },
                    { "4ff4301c-994c-4a95-8a6b-7da2ca1449bb", null, "Stockiest", "STOCKIEST" },
                    { "584b0c55-b7c2-4714-b8de-a32ed9119610", null, "User", "USER" }
                });
        }
    }
}
