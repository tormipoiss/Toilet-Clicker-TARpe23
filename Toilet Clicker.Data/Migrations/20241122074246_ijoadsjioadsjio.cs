using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toilet_Clicker.Data.Migrations
{
    /// <inheritdoc />
    public partial class ijoadsjioadsjio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesToDatabase",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ToiletID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToDatabase", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    LocationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationWasMade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Toilets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToiletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Power = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Speed = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToiletWasBorn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toilets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Toilets_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Toilets_LocationID",
                table: "Toilets",
                column: "LocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesToDatabase");

            migrationBuilder.DropTable(
                name: "Toilets");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
