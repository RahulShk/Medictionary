using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d040969-1198-46a6-aee2-a080a5595cdf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c86cc93-c90e-44a8-a5a6-583c41c0d67d");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Documents",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9e4d1708-3c5b-4f74-9b53-db01a38685e9", null, "admin", "ADMIN" },
                    { "f498cdbc-bd25-4d47-b2c6-a63b4dd00bd3", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e4d1708-3c5b-4f74-9b53-db01a38685e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f498cdbc-bd25-4d47-b2c6-a63b4dd00bd3");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Documents",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d040969-1198-46a6-aee2-a080a5595cdf", null, "admin", "ADMIN" },
                    { "3c86cc93-c90e-44a8-a5a6-583c41c0d67d", null, "user", "USER" }
                });
        }
    }
}
