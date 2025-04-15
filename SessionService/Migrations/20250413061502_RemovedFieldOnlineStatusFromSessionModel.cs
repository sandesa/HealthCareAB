using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SessionService.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFieldOnlineStatusFromSessionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineStatus",
                table: "Sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnlineStatus",
                table: "Sessions",
                type: "bit",
                nullable: true);
        }
    }
}
