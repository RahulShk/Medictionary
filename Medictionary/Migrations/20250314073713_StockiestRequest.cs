using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class StockiestRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14a42148-384d-4fe5-acb8-5fdd068f3083");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56903137-6511-4e32-a7fb-92b9eaa7a8cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eba576cb-35e4-437d-8454-b31589da5700");

            migrationBuilder.CreateTable(
                name: "StockiestRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockiestID = table.Column<string>(type: "text", nullable: false),
                    IndustryID = table.Column<string>(type: "text", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockiestRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_StockiestRequests_AspNetUsers_StockiestID",
                        column: x => x.StockiestID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockiestRequests_Industries_IndustryID",
                        column: x => x.IndustryID,
                        principalTable: "Industries",
                        principalColumn: "IndustryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "195c707d-b887-4427-97db-f55e01f63153", null, "Stockiest", "STOCKIEST" },
                    { "3f55a7f8-eaae-4c36-a036-3140af3ed2ed", null, "User", "USER" },
                    { "a4cce9a9-fd31-4960-bac6-f139cc34ab62", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockiestRequests_IndustryID",
                table: "StockiestRequests",
                column: "IndustryID");

            migrationBuilder.CreateIndex(
                name: "IX_StockiestRequests_StockiestID",
                table: "StockiestRequests",
                column: "StockiestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockiestRequests");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "195c707d-b887-4427-97db-f55e01f63153");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f55a7f8-eaae-4c36-a036-3140af3ed2ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4cce9a9-fd31-4960-bac6-f139cc34ab62");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14a42148-384d-4fe5-acb8-5fdd068f3083", null, "Admin", "ADMIN" },
                    { "56903137-6511-4e32-a7fb-92b9eaa7a8cc", null, "User", "USER" },
                    { "eba576cb-35e4-437d-8454-b31589da5700", null, "Stockiest", "STOCKIEST" }
                });
        }
    }
}
