using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyGoO.Src.Db.Migrations
{
    /// <inheritdoc />
    public partial class boolRaffleLucky : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raffle_Users_CreatedById",
                table: "Raffle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Raffle",
                table: "Raffle");

            migrationBuilder.RenameTable(
                name: "Raffle",
                newName: "Raffles");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Raffles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Raffle_CreatedById",
                table: "Raffles",
                newName: "IX_Raffles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Raffles",
                table: "Raffles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Raffles_Users_UserId",
                table: "Raffles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raffles_Users_UserId",
                table: "Raffles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Raffles",
                table: "Raffles");

            migrationBuilder.RenameTable(
                name: "Raffles",
                newName: "Raffle");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Raffle",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Raffles_UserId",
                table: "Raffle",
                newName: "IX_Raffle_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Raffle",
                table: "Raffle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Raffle_Users_CreatedById",
                table: "Raffle",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
