using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_site_1.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuId",
                table: "RequestPays",
                newName: "Guid");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 11, 2, 15, 7, 56, 788, DateTimeKind.Local).AddTicks(9738));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 11, 2, 15, 7, 56, 788, DateTimeKind.Local).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 11, 2, 15, 7, 56, 788, DateTimeKind.Local).AddTicks(9817));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "RequestPays",
                newName: "GuId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2024, 11, 1, 21, 23, 42, 314, DateTimeKind.Local).AddTicks(9053));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2024, 11, 1, 21, 23, 42, 314, DateTimeKind.Local).AddTicks(9119));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2024, 11, 1, 21, 23, 42, 314, DateTimeKind.Local).AddTicks(9139));
        }
    }
}
