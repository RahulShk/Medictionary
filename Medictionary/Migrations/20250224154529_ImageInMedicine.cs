using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class ImageInMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36ac6180-c73d-46d1-8047-d509c4454f36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f235f5b0-4f6c-4e63-bd8c-a869ef570169");

            migrationBuilder.AddColumn<string>(
                name: "MedicineImageImageId",
                table: "Medicines",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41c55544-77f3-47c2-9ac9-343013877e63", null, "Admin", "ADMIN" },
                    { "cdaee0f0-00fc-45dd-a294-839cb54cd8bf", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineImageImageId",
                table: "Medicines",
                column: "MedicineImageImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Image_MedicineImageImageId",
                table: "Medicines",
                column: "MedicineImageImageId",
                principalTable: "Image",
                principalColumn: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Image_MedicineImageImageId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_MedicineImageImageId",
                table: "Medicines");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41c55544-77f3-47c2-9ac9-343013877e63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdaee0f0-00fc-45dd-a294-839cb54cd8bf");

            migrationBuilder.DropColumn(
                name: "MedicineImageImageId",
                table: "Medicines");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36ac6180-c73d-46d1-8047-d509c4454f36", null, "User", "USER" },
                    { "f235f5b0-4f6c-4e63-bd8c-a869ef570169", null, "Admin", "ADMIN" }
                });
        }
    }
}
