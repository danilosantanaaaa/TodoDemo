using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Todo_Alter_TimeRamainer_To_Null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "TimeRemember",
                table: "Todos",
                type: "time without time zone",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("3a9815f0-018a-41de-8be9-f6dc99e9c632"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 21, 13, 43, 26, 260, DateTimeKind.Utc).AddTicks(1762), new DateTime(2024, 6, 21, 13, 43, 26, 260, DateTimeKind.Utc).AddTicks(1765) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("9724094d-da9c-4939-ba44-ad5851f61bfe"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 21, 13, 43, 26, 260, DateTimeKind.Utc).AddTicks(1768), new DateTime(2024, 6, 21, 13, 43, 26, 260, DateTimeKind.Utc).AddTicks(1769) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("a70ec313-92fa-4965-b1a3-4760b015a03e"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 21, 13, 43, 26, 260, DateTimeKind.Utc).AddTicks(1770), new DateTime(2024, 6, 21, 13, 43, 26, 260, DateTimeKind.Utc).AddTicks(1770) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "TimeRemember",
                table: "Todos",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("3a9815f0-018a-41de-8be9-f6dc99e9c632"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 19, 20, 32, 36, 863, DateTimeKind.Utc).AddTicks(3229), new DateTime(2024, 6, 19, 20, 32, 36, 863, DateTimeKind.Utc).AddTicks(3231) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("9724094d-da9c-4939-ba44-ad5851f61bfe"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 19, 20, 32, 36, 863, DateTimeKind.Utc).AddTicks(3233), new DateTime(2024, 6, 19, 20, 32, 36, 863, DateTimeKind.Utc).AddTicks(3234) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("a70ec313-92fa-4965-b1a3-4760b015a03e"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 19, 20, 32, 36, 863, DateTimeKind.Utc).AddTicks(3235), new DateTime(2024, 6, 19, 20, 32, 36, 863, DateTimeKind.Utc).AddTicks(3235) });
        }
    }
}
