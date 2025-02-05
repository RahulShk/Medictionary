using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24101131-9dc1-4b50-bd3d-4c6d5e69c4bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2810c3c9-d0f6-4346-8c94-b92d2d3907f0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2244927d-b4c8-450a-9b47-34ec8ad38cf3", null, "Admin", "ADMIN" },
                    { "cd65a515-9012-41dc-8b0d-85f3e41f2f66", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2244927d-b4c8-450a-9b47-34ec8ad38cf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd65a515-9012-41dc-8b0d-85f3e41f2f66");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24101131-9dc1-4b50-bd3d-4c6d5e69c4bd", null, "user", "USER" },
                    { "2810c3c9-d0f6-4346-8c94-b92d2d3907f0", null, "admin", "ADMIN" }
                });
        }
    }
}
