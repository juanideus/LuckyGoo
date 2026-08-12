using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyGoO.Src.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantityOfTickets",
                table: "Raffles",
                newName: "QuantityOfTickets");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Raffles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Raffles");

            migrationBuilder.RenameColumn(
                name: "QuantityOfTickets",
                table: "Raffles",
                newName: "quantityOfTickets");
        }
    }
}
