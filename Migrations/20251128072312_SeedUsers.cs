using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleApi.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthday", "Name", "Phone", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1992, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "測試用戶", "0900111222", new DateTime(2025, 11, 28, 15, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthday", "Name", "Phone", "UpdatedAt" },
                values: new object[] { 2, new DateTime(1988, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "行政助理", "0922333444", new DateTime(2025, 11, 28, 15, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
