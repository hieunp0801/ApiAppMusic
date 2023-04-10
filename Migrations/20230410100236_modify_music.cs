using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAppMusic.Migrations
{
    public partial class modify_music : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_musics_singers_singerId",
                table: "musics");

            migrationBuilder.RenameColumn(
                name: "singerId",
                table: "musics",
                newName: "SingerId");

            migrationBuilder.RenameIndex(
                name: "IX_musics_singerId",
                table: "musics",
                newName: "IX_musics_SingerId");

            migrationBuilder.AlterColumn<int>(
                name: "SingerId",
                table: "musics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_musics_singers_SingerId",
                table: "musics",
                column: "SingerId",
                principalTable: "singers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_musics_singers_SingerId",
                table: "musics");

            migrationBuilder.RenameColumn(
                name: "SingerId",
                table: "musics",
                newName: "singerId");

            migrationBuilder.RenameIndex(
                name: "IX_musics_SingerId",
                table: "musics",
                newName: "IX_musics_singerId");

            migrationBuilder.AlterColumn<int>(
                name: "singerId",
                table: "musics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_musics_singers_singerId",
                table: "musics",
                column: "singerId",
                principalTable: "singers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
