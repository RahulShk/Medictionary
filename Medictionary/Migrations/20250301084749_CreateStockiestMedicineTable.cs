using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class CreateStockiestMedicineTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "099d0ddf-4705-408c-af8b-656f5595a8d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "483166d2-9778-4fa8-ab29-4fe733d59516");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb8efd16-a4eb-404e-b4a0-3b7d184c2045");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "099d0ddf-4705-408c-af8b-656f5595a8d9", null, "Stockiest", "STOCKIEST" },
                    { "483166d2-9778-4fa8-ab29-4fe733d59516", null, "User", "USER" },
                    { "cb8efd16-a4eb-404e-b4a0-3b7d184c2045", null, "Admin", "ADMIN" }
                });
        }
    }
}
