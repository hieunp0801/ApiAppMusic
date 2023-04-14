using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAppMusic.Migrations
{
    public partial class user_4141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "playlists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_playlists_userId",
                table: "playlists",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_playlists_AspNetUsers_userId",
                table: "playlists",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playlists_AspNetUsers_userId",
                table: "playlists");

            migrationBuilder.DropIndex(
                name: "IX_playlists_userId",
                table: "playlists");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "playlists");
        }
    }
}
