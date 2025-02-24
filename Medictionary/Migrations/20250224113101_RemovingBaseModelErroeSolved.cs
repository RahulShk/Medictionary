using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class RemovingBaseModelErroeSolved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f8403af-3ee0-4281-b8c8-8390ec597525");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcf4f163-678d-4f6b-b479-4b88908e1273");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36ac6180-c73d-46d1-8047-d509c4454f36", null, "User", "USER" },
                    { "f235f5b0-4f6c-4e63-bd8c-a869ef570169", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36ac6180-c73d-46d1-8047-d509c4454f36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f235f5b0-4f6c-4e63-bd8c-a869ef570169");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f8403af-3ee0-4281-b8c8-8390ec597525", null, "Admin", "ADMIN" },
                    { "dcf4f163-678d-4f6b-b479-4b88908e1273", null, "User", "USER" }
                });
        }
    }
}
