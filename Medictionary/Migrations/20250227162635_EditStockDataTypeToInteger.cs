using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class EditStockDataTypeToInteger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83f31ecf-a812-4103-9030-1ccce71f131a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b50bb0d0-fb34-4a04-9e2f-95b734bc9b57");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "642e83b8-d9fb-4e4e-bcc0-27c2f0c6cae9", null, "Admin", "ADMIN" },
                    { "dde1454a-129c-431c-91c3-d7e41e89002c", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "642e83b8-d9fb-4e4e-bcc0-27c2f0c6cae9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dde1454a-129c-431c-91c3-d7e41e89002c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "83f31ecf-a812-4103-9030-1ccce71f131a", null, "Admin", "ADMIN" },
                    { "b50bb0d0-fb34-4a04-9e2f-95b734bc9b57", null, "User", "USER" }
                });
        }
    }
}
