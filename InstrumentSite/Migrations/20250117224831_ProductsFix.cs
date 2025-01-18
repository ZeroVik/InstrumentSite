using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class ProductsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1634), "$2a$11$HAPz25iFVlyFZ1WGOSYfYOb.LgyYme06vl0argz1Iarolrmo74IfC", new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1639) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 22, 41, 12, 890, DateTimeKind.Utc).AddTicks(148), "$2a$11$Fg7FAOD1Hz3adtcqJfSG9.pQrKMJc.qvwkkZyR2eA8JrtOLz.jsiS", new DateTime(2025, 1, 17, 22, 41, 12, 890, DateTimeKind.Utc).AddTicks(152) });
        }
    }
}
