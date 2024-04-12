using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library_management.Migrations
{
    /// <inheritdoc />
    public partial class DataDB_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be2a3032-c262-4036-b2dc-382c26306ee5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb7ff408-bee9-4da9-9afe-d3c3eda480a2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56be59ef-bfb3-45dd-9d8b-c1d60e0c5ad4", null, "client", "client" },
                    { "6a032ac5-734e-446a-ba23-4b6212702f9e", null, "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56be59ef-bfb3-45dd-9d8b-c1d60e0c5ad4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a032ac5-734e-446a-ba23-4b6212702f9e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "be2a3032-c262-4036-b2dc-382c26306ee5", null, "client", null },
                    { "cb7ff408-bee9-4da9-9afe-d3c3eda480a2", null, "admin", "client" }
                });
        }
    }
}
