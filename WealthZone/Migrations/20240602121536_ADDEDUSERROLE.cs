using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WealthZone.Migrations
{
    /// <inheritdoc />
    public partial class ADDEDUSERROLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cbbc47c-8f1c-4c7f-bb25-b7aaec782ea0", null, "Admin", "ADMIN" },
                    { "5829de8f-4a7f-49bd-9be9-11d14c1fb3ae", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cbbc47c-8f1c-4c7f-bb25-b7aaec782ea0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5829de8f-4a7f-49bd-9be9-11d14c1fb3ae");
        }
    }
}
