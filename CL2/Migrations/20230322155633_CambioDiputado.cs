using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class CambioDiputado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Diputado_Cedula",
                table: "Diputado");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "Diputado");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Diputado");

            migrationBuilder.AddColumn<string>(
                name: "TelefonoDiputado2",
                table: "Diputado",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelefonoDiputado2",
                table: "Diputado");

            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "Diputado",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Diputado",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Diputado_Cedula",
                table: "Diputado",
                column: "Cedula",
                unique: true);
        }
    }
}
