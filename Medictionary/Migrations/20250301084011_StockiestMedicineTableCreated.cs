using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class StockiestMedicineTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "642e83b8-d9fb-4e4e-bcc0-27c2f0c6cae9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dde1454a-129c-431c-91c3-d7e41e89002c");

            migrationBuilder.CreateTable(
                name: "StockiestMedicines",
                columns: table => new
                {
                    StockiestID = table.Column<string>(type: "text", nullable: false),
                    MedicineID = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockiestMedicines", x => new { x.StockiestID, x.MedicineID });
                    table.ForeignKey(
                        name: "FK_StockiestMedicines_AspNetUsers_StockiestID",
                        column: x => x.StockiestID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockiestMedicines_Medicines_MedicineID",
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
                    { "099d0ddf-4705-408c-af8b-656f5595a8d9", null, "Stockiest", "STOCKIEST" },
                    { "483166d2-9778-4fa8-ab29-4fe733d59516", null, "User", "USER" },
                    { "cb8efd16-a4eb-404e-b4a0-3b7d184c2045", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockiestMedicines_MedicineID",
                table: "StockiestMedicines",
                column: "MedicineID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockiestMedicines");

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
                    { "642e83b8-d9fb-4e4e-bcc0-27c2f0c6cae9", null, "Admin", "ADMIN" },
                    { "dde1454a-129c-431c-91c3-d7e41e89002c", null, "User", "USER" }
                });
        }
    }
}
