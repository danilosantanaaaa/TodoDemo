using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Todo_Addd_Field_FinishedOnUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedOnUtc",
                table: "Todos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedOnUtc",
                table: "Todos");

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("3a9815f0-018a-41de-8be9-f6dc99e9c632"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 18, 17, 39, 38, 863, DateTimeKind.Utc).AddTicks(9111), new DateTime(2024, 6, 18, 17, 39, 38, 863, DateTimeKind.Utc).AddTicks(9114) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("9724094d-da9c-4939-ba44-ad5851f61bfe"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 18, 17, 39, 38, 863, DateTimeKind.Utc).AddTicks(9117), new DateTime(2024, 6, 18, 17, 39, 38, 863, DateTimeKind.Utc).AddTicks(9117) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: new Guid("a70ec313-92fa-4965-b1a3-4760b015a03e"),
                columns: new[] { "CreatedOnUtc", "UpdatedOnUtc" },
                values: new object[] { new DateTime(2024, 6, 18, 17, 39, 38, 863, DateTimeKind.Utc).AddTicks(9119), new DateTime(2024, 6, 18, 17, 39, 38, 863, DateTimeKind.Utc).AddTicks(9119) });
        }
    }
}
