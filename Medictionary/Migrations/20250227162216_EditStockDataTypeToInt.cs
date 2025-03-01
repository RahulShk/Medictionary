using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medictionary.Migrations
{
    /// <inheritdoc />
    public partial class EditStockDataTypeToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41c55544-77f3-47c2-9ac9-343013877e63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdaee0f0-00fc-45dd-a294-839cb54cd8bf");

            // Remove the default value
            migrationBuilder.Sql("ALTER TABLE \"Medicines\" ALTER COLUMN \"Stock\" DROP DEFAULT;");

            // Alter the column type
            migrationBuilder.Sql("ALTER TABLE \"Medicines\" ALTER COLUMN \"Stock\" TYPE integer USING \"Stock\"::integer;");

            // Optionally, re-add the default value if needed
            // migrationBuilder.Sql("ALTER TABLE \"Medicines\" ALTER COLUMN \"Stock\" SET DEFAULT 0;");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "83f31ecf-a812-4103-9030-1ccce71f131a", null, "Admin", "ADMIN" },
                    { "b50bb0d0-fb34-4a04-9e2f-95b734bc9b57", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83f31ecf-a812-4103-9030-1ccce71f131a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b50bb0d0-fb34-4a04-9e2f-95b734bc9b57");

            // Revert the column type
            migrationBuilder.AlterColumn<string>(
                name: "Stock",
                table: "Medicines",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41c55544-77f3-47c2-9ac9-343013877e63", null, "Admin", "ADMIN" },
                    { "cdaee0f0-00fc-45dd-a294-839cb54cd8bf", null, "User", "USER" }
                });
        }
    }
}