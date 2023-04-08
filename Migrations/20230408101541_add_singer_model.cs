using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAppMusic.Migrations
{
    public partial class add_singer_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "singers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSinger = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageSinger = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateofBirth = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_singers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "singers");
        }
    }
}
