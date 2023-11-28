using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItTakesAVillage.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseClassForEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DinnerInvitations",
                table: "DinnerInvitations");

            migrationBuilder.RenameTable(
                name: "DinnerInvitations",
                newName: "Events");

            migrationBuilder.AddColumn<int>(
                name: "RelatedEventId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Events",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RelatedEventId",
                table: "Notifications",
                column: "RelatedEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Events_RelatedEventId",
                table: "Notifications",
                column: "RelatedEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Events_RelatedEventId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_RelatedEventId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RelatedEventId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "DinnerInvitations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DinnerInvitations",
                table: "DinnerInvitations",
                column: "Id");
        }
    }
}
