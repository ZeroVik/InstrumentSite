using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Carts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 27, 19, 28, 27, 366, DateTimeKind.Utc).AddTicks(5701), "$2a$11$HHFUpodD2Cu0L7ZTjjZHw.hcD85odE7qyx97MM28aQgq/C9oNPMES", new DateTime(2025, 1, 27, 19, 28, 27, 366, DateTimeKind.Utc).AddTicks(5704) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 19, 16, 18, 32, 355, DateTimeKind.Utc).AddTicks(4206), "$2a$11$WCFVgOuqtHO2nVsWN0api.F0f3xU/RUjviHm8rTPfF.a35OsnzG.K", new DateTime(2025, 1, 19, 16, 18, 32, 355, DateTimeKind.Utc).AddTicks(4210) });
        }
    }
}
