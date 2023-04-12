using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAppMusic.Migrations
{
    public partial class playlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "musicPlaylists",
                columns: table => new
                {
                    IdMusic = table.Column<int>(type: "int", nullable: false),
                    IdPlaylist = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musicPlaylists", x => new { x.IdMusic, x.IdPlaylist });
                    table.ForeignKey(
                        name: "FK_musicPlaylists_musics_IdMusic",
                        column: x => x.IdMusic,
                        principalTable: "musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_musicPlaylists_playlists_IdPlaylist",
                        column: x => x.IdPlaylist,
                        principalTable: "playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_musicPlaylists_IdPlaylist",
                table: "musicPlaylists",
                column: "IdPlaylist");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "musicPlaylists");

            migrationBuilder.DropTable(
                name: "playlists");
        }
    }
}
