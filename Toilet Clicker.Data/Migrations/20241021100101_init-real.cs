using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toilet_Clicker.Data.Migrations
{
    /// <inheritdoc />
    public partial class initreal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Toilets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToiletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Power = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Speed = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ToiletWasBorn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toilets", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Toilets");
        }
    }
}
