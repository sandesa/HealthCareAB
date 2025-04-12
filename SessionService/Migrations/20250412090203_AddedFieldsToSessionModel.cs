using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SessionService.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsToSessionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Sessions",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpiresIn",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ExpiresIn",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Sessions",
                newName: "Token");
        }
    }
}
