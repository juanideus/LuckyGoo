using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LuckyGoO.Src.Db.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeRaffleNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumbersOfLucky",
                table: "Raffles");

            migrationBuilder.DropColumn(
                name: "WinningNumbers",
                table: "Raffles");

            migrationBuilder.CreateTable(
                name: "RaffleNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    RaffleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaffleNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaffleNumbers_Raffles_RaffleId",
                        column: x => x.RaffleId,
                        principalTable: "Raffles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaffleNumbers_RaffleId_Number_Type",
                table: "RaffleNumbers",
                columns: new[] { "RaffleId", "Number", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaffleNumbers");

            migrationBuilder.AddColumn<List<int>>(
                name: "NumbersOfLucky",
                table: "Raffles",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "WinningNumbers",
                table: "Raffles",
                type: "integer[]",
                nullable: false);
        }
    }
}
