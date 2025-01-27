using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdToIntAndAddRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 19, 21, 14, 37, 287, DateTimeKind.Utc).AddTicks(1571), "$2a$11$XHShHsTVsaMUCNOpciKcCetAZyybTrh0jM7TaF.5j1ImDFJVjpoQy", new DateTime(2025, 1, 19, 21, 14, 37, 287, DateTimeKind.Utc).AddTicks(1576) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 19, 16, 18, 32, 355, DateTimeKind.Utc).AddTicks(4206), "$2a$11$WCFVgOuqtHO2nVsWN0api.F0f3xU/RUjviHm8rTPfF.a35OsnzG.K", new DateTime(2025, 1, 19, 16, 18, 32, 355, DateTimeKind.Utc).AddTicks(4210) });
        }
    }
}
