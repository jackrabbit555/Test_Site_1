using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_site_1.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_ViewCount_To_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 7, 28, 16, 24, 42, 807, DateTimeKind.Local).AddTicks(2655));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 7, 28, 16, 24, 42, 807, DateTimeKind.Local).AddTicks(2717));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 7, 28, 16, 24, 42, 807, DateTimeKind.Local).AddTicks(2736));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 23, 12, 55, 14, 271, DateTimeKind.Local).AddTicks(1140));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 23, 12, 55, 14, 271, DateTimeKind.Local).AddTicks(1184));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 5, 23, 12, 55, 14, 271, DateTimeKind.Local).AddTicks(1198));
        }
    }
}
