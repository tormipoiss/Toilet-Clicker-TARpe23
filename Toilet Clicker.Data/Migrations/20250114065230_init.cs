using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toilet_Clicker.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Toilets",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnershipCreatedAt",
                table: "Toilets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnershipID",
                table: "Toilets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnershipUpdatedAt",
                table: "Toilets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerProfileID",
                table: "Toilets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerProfileID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PlayerProfiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStatus = table.Column<int>(type: "int", nullable: false),
                    ProfileType = table.Column<bool>(type: "bit", nullable: false),
                    ProfileCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileAttributedToAnAccountUserAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileStatusLastChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerProfiles", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Toilets_PlayerProfileID",
                table: "Toilets",
                column: "PlayerProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Toilets_PlayerProfiles_PlayerProfileID",
                table: "Toilets",
                column: "PlayerProfileID",
                principalTable: "PlayerProfiles",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toilets_PlayerProfiles_PlayerProfileID",
                table: "Toilets");

            migrationBuilder.DropTable(
                name: "PlayerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Toilets_PlayerProfileID",
                table: "Toilets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Toilets");

            migrationBuilder.DropColumn(
                name: "OwnershipCreatedAt",
                table: "Toilets");

            migrationBuilder.DropColumn(
                name: "OwnershipID",
                table: "Toilets");

            migrationBuilder.DropColumn(
                name: "OwnershipUpdatedAt",
                table: "Toilets");

            migrationBuilder.DropColumn(
                name: "PlayerProfileID",
                table: "Toilets");

            migrationBuilder.DropColumn(
                name: "PlayerProfileID",
                table: "AspNetUsers");
        }
    }
}
