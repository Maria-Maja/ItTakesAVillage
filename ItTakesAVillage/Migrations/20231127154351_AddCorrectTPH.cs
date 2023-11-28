using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItTakesAVillage.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectTPH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Events",
                newName: "EventType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventType",
                table: "Events",
                newName: "Discriminator");
        }
    }
}
