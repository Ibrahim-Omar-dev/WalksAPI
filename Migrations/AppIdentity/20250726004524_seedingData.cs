using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WalksAPI.Migrations.AppIdentity
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07b68a9b-4c46-4235-b84a-1d8a0690c6ef", "07b68a9b-4c46-4235-b84a-1d8a0690c6ef", "User", "USER" },
                    { "c85c6b59-72fa-4db0-9de9-69e462384f7f", "c85c6b59-72fa-4db0-9de9-69e462384f7f", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07b68a9b-4c46-4235-b84a-1d8a0690c6ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c85c6b59-72fa-4db0-9de9-69e462384f7f");
        }
    }
}
