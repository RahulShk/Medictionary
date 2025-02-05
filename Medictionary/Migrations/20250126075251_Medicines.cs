using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class Medicines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2244927d-b4c8-450a-9b47-34ec8ad38cf3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd65a515-9012-41dc-8b0d-85f3e41f2f66");

            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Composition",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpiryDate",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryID",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturingDate",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medicine_Name",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Documents",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stock",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38e51472-e7df-43eb-b3c4-a050a40e7186", null, "User", "USER" },
                    { "c49494cc-51e1-47a9-830d-c8fa514b1bea", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38e51472-e7df-43eb-b3c4-a050a40e7186");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c49494cc-51e1-47a9-830d-c8fa514b1bea");

            migrationBuilder.DropColumn(
                name: "Batch",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Composition",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "IndustryID",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ManufacturingDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Medicine_Name",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Documents");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2244927d-b4c8-450a-9b47-34ec8ad38cf3", null, "Admin", "ADMIN" },
                    { "cd65a515-9012-41dc-8b0d-85f3e41f2f66", null, "User", "USER" }
                });
        }
    }
}
