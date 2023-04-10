using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAppMusic.Migrations
{
    public partial class music : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "singerId",
                table: "musics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_musics_singerId",
                table: "musics",
                column: "singerId");

            migrationBuilder.AddForeignKey(
                name: "FK_musics_singers_singerId",
                table: "musics",
                column: "singerId",
                principalTable: "singers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_musics_singers_singerId",
                table: "musics");

            migrationBuilder.DropIndex(
                name: "IX_musics_singerId",
                table: "musics");

            migrationBuilder.DropColumn(
                name: "singerId",
                table: "musics");
        }
    }
}
