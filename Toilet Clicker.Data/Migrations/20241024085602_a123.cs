﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toilet_Clicker.Data.Migrations
{
    /// <inheritdoc />
    public partial class a123 : Migration
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
                    ToiletID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToDatabase", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesToDatabase");
        }
    }
}
