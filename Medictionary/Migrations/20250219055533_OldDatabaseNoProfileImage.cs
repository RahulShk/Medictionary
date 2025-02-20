using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class OldDatabaseNoProfileImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70dc7904-dd8d-4ed2-8b04-b23dca805373");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7304f810-885a-4230-8b08-889a9b90557a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "204f51c9-04eb-4633-83b7-3ff0ba0b6a10", null, "Admin", "ADMIN" },
                    { "cd29f973-308e-4575-a7cc-b7a80b7344ab", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_IndustryID",
                table: "Documents",
                column: "IndustryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Documents_IndustryID",
                table: "Documents",
                column: "IndustryID",
                principalTable: "Documents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Documents_IndustryID",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_IndustryID",
                table: "Documents");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "204f51c9-04eb-4633-83b7-3ff0ba0b6a10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd29f973-308e-4575-a7cc-b7a80b7344ab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70dc7904-dd8d-4ed2-8b04-b23dca805373", null, "Admin", "ADMIN" },
                    { "7304f810-885a-4230-8b08-889a9b90557a", null, "User", "USER" }
                });
        }
    }
}
