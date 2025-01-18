using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 18, 1, 44, 14, 649, DateTimeKind.Utc).AddTicks(4357), "$2a$11$jHyTjutoGesr7EEJzFIs3OgWrDn4CfG9eW7i.5A9AspcEy.WCOny.", new DateTime(2025, 1, 18, 1, 44, 14, 649, DateTimeKind.Utc).AddTicks(4362) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1634), "$2a$11$HAPz25iFVlyFZ1WGOSYfYOb.LgyYme06vl0argz1Iarolrmo74IfC", new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1639) });
        }
    }
}
