using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_site_1.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddHomePageImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageLocation",
                table: "HomePageImages",
                newName: "ImageLocation");

            migrationBuilder.AlterColumn<string>(
                name: "Src",
                table: "HomePageImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 10, 23, 13, 39, 27, 478, DateTimeKind.Local).AddTicks(8264));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 10, 23, 13, 39, 27, 478, DateTimeKind.Local).AddTicks(8322));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 10, 23, 13, 39, 27, 478, DateTimeKind.Local).AddTicks(8341));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLocation",
                table: "HomePageImages",
                newName: "imageLocation");

            migrationBuilder.AlterColumn<int>(
                name: "Src",
                table: "HomePageImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 10, 16, 9, 49, 53, 834, DateTimeKind.Local).AddTicks(1147));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 10, 16, 9, 49, 53, 834, DateTimeKind.Local).AddTicks(1246));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 10, 16, 9, 49, 53, 834, DateTimeKind.Local).AddTicks(1280));
        }
    }
}
