using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace score_management_be.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNoteInTheConductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Conducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Conducts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
