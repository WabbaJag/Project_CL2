using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class AgregarPosicionControlP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosicionControl",
                table: "ControlPolitico",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosicionControl",
                table: "ControlPolitico");
        }
    }
}
