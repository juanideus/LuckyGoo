using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LuckyGoO.Src.Db.Migrations
{
    /// <inheritdoc />
    public partial class raffle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Raffle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOfRaffle = table.Column<DateOnly>(type: "date", nullable: false),
                    quantityOfTickets = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<int>(type: "integer", nullable: false),
                    SubTotalWhitLucky = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    WinningNumbers = table.Column<List<int>>(type: "integer[]", nullable: false),
                    NumbersOfLucky = table.Column<List<int>>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raffle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raffle_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Raffle_CreatedById",
                table: "Raffle",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raffle");
        }
    }
}
