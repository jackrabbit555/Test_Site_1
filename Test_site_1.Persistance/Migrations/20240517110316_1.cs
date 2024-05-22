using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_site_1.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 17, 14, 33, 15, 358, DateTimeKind.Local).AddTicks(4046));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 17, 14, 33, 15, 358, DateTimeKind.Local).AddTicks(4087));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 17, 14, 33, 15, 358, DateTimeKind.Local).AddTicks(4099));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 17, 14, 32, 7, 573, DateTimeKind.Local).AddTicks(3701));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 17, 14, 32, 7, 573, DateTimeKind.Local).AddTicks(3784));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 17, 14, 32, 7, 573, DateTimeKind.Local).AddTicks(3798));
        }
    }
}
