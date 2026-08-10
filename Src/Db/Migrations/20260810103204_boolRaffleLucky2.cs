using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyGoO.Src.Db.Migrations
{
    /// <inheritdoc />
    public partial class boolRaffleLucky2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLuckyRaffle",
                table: "Raffles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLuckyRaffle",
                table: "Raffles");
        }
    }
}
