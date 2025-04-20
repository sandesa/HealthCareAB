using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SessionService.Migrations
{
    /// <inheritdoc />
    public partial class FieldTypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresIn",
                table: "Sessions");

            migrationBuilder.AddColumn<DateTime>(
                name: "Expires",
                table: "Sessions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expires",
                table: "Sessions");

            migrationBuilder.AddColumn<int>(
                name: "ExpiresIn",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
