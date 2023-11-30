using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItTakesAVillage.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorIdToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Groups");
        }
    }
}
