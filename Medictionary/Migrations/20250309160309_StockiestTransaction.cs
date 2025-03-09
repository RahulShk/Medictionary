using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class StockiestTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "StockiestTransactionRecords",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockiestID = table.Column<string>(type: "text", nullable: false),
                    MedicineID = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    MRP = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockiestTransactionRecords", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_StockiestTransactionRecords_AspNetUsers_StockiestID",
                        column: x => x.StockiestID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockiestTransactionRecords_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14a42148-384d-4fe5-acb8-5fdd068f3083", null, "Admin", "ADMIN" },
                    { "56903137-6511-4e32-a7fb-92b9eaa7a8cc", null, "User", "USER" },
                    { "eba576cb-35e4-437d-8454-b31589da5700", null, "Stockiest", "STOCKIEST" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockiestTransactionRecords_MedicineID",
                table: "StockiestTransactionRecords",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_StockiestTransactionRecords_StockiestID",
                table: "StockiestTransactionRecords",
                column: "StockiestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockiestTransactionRecords");

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
    }
}
