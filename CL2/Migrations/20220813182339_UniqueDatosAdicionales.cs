using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class UniqueDatosAdicionales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnnoLegislativo",
                table: "Legislativo",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreFraccion",
                table: "Fraccion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AdministracionPeriodo",
                table: "Administracion",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Legislativo_AnnoLegislativo",
                table: "Legislativo",
                column: "AnnoLegislativo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fraccion_NombreFraccion",
                table: "Fraccion",
                column: "NombreFraccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administracion_AdministracionPeriodo",
                table: "Administracion",
                column: "AdministracionPeriodo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Legislativo_AnnoLegislativo",
                table: "Legislativo");

            migrationBuilder.DropIndex(
                name: "IX_Fraccion_NombreFraccion",
                table: "Fraccion");

            migrationBuilder.DropIndex(
                name: "IX_Administracion_AdministracionPeriodo",
                table: "Administracion");

            migrationBuilder.AlterColumn<string>(
                name: "AnnoLegislativo",
                table: "Legislativo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreFraccion",
                table: "Fraccion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AdministracionPeriodo",
                table: "Administracion",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
