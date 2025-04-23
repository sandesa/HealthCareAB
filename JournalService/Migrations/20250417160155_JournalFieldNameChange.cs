using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalService.Migrations
{
    /// <inheritdoc />
    public partial class JournalFieldNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JournalText",
                table: "Journals",
                newName: "JournalEntry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JournalEntry",
                table: "Journals",
                newName: "JournalText");
        }
    }
}
