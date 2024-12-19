using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dec8802-43e5-4307-921b-60bc24c18a17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4211617-47bc-44e4-b4e6-bec971a313fa");

            migrationBuilder.DropColumn(
                name: "last_updated_by",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "last_updated_date",
                table: "Documents");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24101131-9dc1-4b50-bd3d-4c6d5e69c4bd", null, "user", "USER" },
                    { "2810c3c9-d0f6-4346-8c94-b92d2d3907f0", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24101131-9dc1-4b50-bd3d-4c6d5e69c4bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2810c3c9-d0f6-4346-8c94-b92d2d3907f0");

            migrationBuilder.AddColumn<string>(
                name: "last_updated_by",
                table: "Documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_updated_date",
                table: "Documents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dec8802-43e5-4307-921b-60bc24c18a17", null, "user", "USER" },
                    { "e4211617-47bc-44e4-b4e6-bec971a313fa", null, "admin", "ADMIN" }
                });
        }
    }
}
