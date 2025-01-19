using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 19, 16, 18, 32, 355, DateTimeKind.Utc).AddTicks(4206), "$2a$11$WCFVgOuqtHO2nVsWN0api.F0f3xU/RUjviHm8rTPfF.a35OsnzG.K", new DateTime(2025, 1, 19, 16, 18, 32, 355, DateTimeKind.Utc).AddTicks(4210) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 19, 15, 50, 0, 94, DateTimeKind.Utc).AddTicks(7527), "$2a$11$tj2FESK26otRdUVwVa3bYuKD63oj0OeP1hECRnTYhXCEAG36xrJwe", new DateTime(2025, 1, 19, 15, 50, 0, 94, DateTimeKind.Utc).AddTicks(7531) });
        }
    }
}
