using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class comentarioSesion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoComentario",
                table: "DiscusionPlenario",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoComentario",
                table: "DiscusionPlenario");
        }
    }
}
