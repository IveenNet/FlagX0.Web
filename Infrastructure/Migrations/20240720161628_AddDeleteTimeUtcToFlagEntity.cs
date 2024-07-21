using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlagX0.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteTimeUtcToFlagEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTimeUtc",
                table: "Flags",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Flags",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteTimeUtc",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Flags");
        }
    }
}
