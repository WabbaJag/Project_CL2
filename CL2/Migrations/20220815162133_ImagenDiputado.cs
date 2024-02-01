using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class ImagenDiputado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cedula",
                table: "Diputado",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Diputado",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diputado_Cedula",
                table: "Diputado",
                column: "Cedula",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Diputado_Cedula",
                table: "Diputado");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Diputado");

            migrationBuilder.AlterColumn<string>(
                name: "Cedula",
                table: "Diputado",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
