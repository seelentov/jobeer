using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobeer.Migrations
{
    /// <inheritdoc />
    public partial class Clear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotifCache");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SearchModels");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SearchModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SearchModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SearchModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NotifCache",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifCache", x => x.Id);
                });
        }
    }
}
