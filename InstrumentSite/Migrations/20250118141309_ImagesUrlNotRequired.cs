using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class ImagesUrlNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 18, 14, 13, 9, 321, DateTimeKind.Utc).AddTicks(8464), "$2a$11$3tayGbOPi0UteQGgssPR/e4WK5ljrptomBM8eelbx9ukpnJSvtO8a", new DateTime(2025, 1, 18, 14, 13, 9, 321, DateTimeKind.Utc).AddTicks(8468) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 18, 14, 10, 57, 632, DateTimeKind.Utc).AddTicks(8231), "$2a$11$tRBVfnpf9zd8FbYeeQhFyu4kTBk96gKHQeqao.UGWd3zlNtfw83DS", new DateTime(2025, 1, 18, 14, 10, 57, 632, DateTimeKind.Utc).AddTicks(8235) });
        }
    }
}
