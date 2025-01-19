using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class SecondHand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSecondHand",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "IsSecondHand" },
                values: new object[] { "uploads/Guitar.jpg", false });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsSecondHand",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 19, 15, 50, 0, 94, DateTimeKind.Utc).AddTicks(7527), "$2a$11$tj2FESK26otRdUVwVa3bYuKD63oj0OeP1hECRnTYhXCEAG36xrJwe", new DateTime(2025, 1, 19, 15, 50, 0, 94, DateTimeKind.Utc).AddTicks(7531) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSecondHand",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.mm.bing.net%2Fth%3Fid%3DOIP.jUzKhxeqrkYWFMhyG47wTwHaLx%26pid%3DApi&f=1&ipt=e3d60bc60776244798485bf9bcec423362c9dc8ebd322f7956048a0a7b8602b7&ipo=images");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1634), "$2a$11$HAPz25iFVlyFZ1WGOSYfYOb.LgyYme06vl0argz1Iarolrmo74IfC", new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1639) });
        }
    }
}
