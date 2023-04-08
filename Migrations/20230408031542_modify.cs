using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAppMusic.Migrations
{
    public partial class modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "musics",
                newName: "NameMusic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameMusic",
                table: "musics",
                newName: "Name");
        }
    }
}
