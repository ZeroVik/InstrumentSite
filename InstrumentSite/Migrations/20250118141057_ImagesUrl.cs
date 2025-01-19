using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentSite.Migrations
{
    /// <inheritdoc />
    public partial class ImagesUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "Uploads/Guitar.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "Uploads/Guitar.jpg");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 18, 14, 10, 57, 632, DateTimeKind.Utc).AddTicks(8231), "$2a$11$tRBVfnpf9zd8FbYeeQhFyu4kTBk96gKHQeqao.UGWd3zlNtfw83DS", new DateTime(2025, 1, 18, 14, 10, 57, 632, DateTimeKind.Utc).AddTicks(8235) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.mm.bing.net%2Fth%3Fid%3DOIP.jUzKhxeqrkYWFMhyG47wTwHaLx%26pid%3DApi&f=1&ipt=e3d60bc60776244798485bf9bcec423362c9dc8ebd322f7956048a0a7b8602b7&ipo=images");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.GDvI_nVc29ofgxfXFao1vwHaHa%26pid%3DApi&f=1&ipt=1fec529b0b28d922c840939fbff720313f7075d9b04d2fb5bf8c45cd07137c17&ipo=images");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1634), "$2a$11$HAPz25iFVlyFZ1WGOSYfYOb.LgyYme06vl0argz1Iarolrmo74IfC", new DateTime(2025, 1, 17, 22, 48, 30, 914, DateTimeKind.Utc).AddTicks(1639) });
        }
    }
}
