using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WealthZone.Migrations
{
    /// <inheritdoc />
    public partial class oneTooneRelationhipWithComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f6b8ef0-eab5-421f-94e9-3db6674924b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdd88188-3c0f-4da8-83eb-0726600dd83e");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b021712e-d118-49d8-94f8-db69887e4288", null, "Admin", "ADMIN" },
                    { "d4e28151-eb4b-4366-931e-94ba6c89f266", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_AppUserId",
                table: "comments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_AppUserId",
                table: "comments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_AppUserId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_AppUserId",
                table: "comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b021712e-d118-49d8-94f8-db69887e4288");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4e28151-eb4b-4366-931e-94ba6c89f266");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f6b8ef0-eab5-421f-94e9-3db6674924b2", null, "User", "USER" },
                    { "fdd88188-3c0f-4da8-83eb-0726600dd83e", null, "Admin", "ADMIN" }
                });
        }
    }
}
