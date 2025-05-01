using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SessionService.Migrations
{
    /// <inheritdoc />
    public partial class TypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sessions");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Sessions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sessions");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
