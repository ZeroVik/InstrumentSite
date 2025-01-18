using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class ProductsImageURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 22, 41, 12, 890, DateTimeKind.Utc).AddTicks(148), "$2a$11$Fg7FAOD1Hz3adtcqJfSG9.pQrKMJc.qvwkkZyR2eA8JrtOLz.jsiS", new DateTime(2025, 1, 17, 22, 41, 12, 890, DateTimeKind.Utc).AddTicks(152) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 22, 22, 18, 205, DateTimeKind.Utc).AddTicks(4446), "$2a$11$gANisKKDuqIGHIkj16xB7.5qgkOt3Ae8F/zRh4GNC4hhQTRPal/Cy", new DateTime(2025, 1, 17, 22, 22, 18, 205, DateTimeKind.Utc).AddTicks(4450) });
        }
    }
}
