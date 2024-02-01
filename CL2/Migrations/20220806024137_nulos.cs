using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class @null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntegrantesPlenario_Fraccion_FraccionID",
                table: "IntegrantesPlenario");

            migrationBuilder.DropIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario");

            migrationBuilder.AlterColumn<int>(
                name: "FraccionID",
                table: "IntegrantesPlenario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario",
                column: "FraccionID",
                unique: true,
                filter: "[FraccionID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_IntegrantesPlenario_Fraccion_FraccionID",
                table: "IntegrantesPlenario",
                column: "FraccionID",
                principalTable: "Fraccion",
                principalColumn: "FraccionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntegrantesPlenario_Fraccion_FraccionID",
                table: "IntegrantesPlenario");

            migrationBuilder.DropIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario");

            migrationBuilder.AlterColumn<int>(
                name: "FraccionID",
                table: "IntegrantesPlenario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario",
                column: "FraccionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegrantesPlenario_Fraccion_FraccionID",
                table: "IntegrantesPlenario",
                column: "FraccionID",
                principalTable: "Fraccion",
                principalColumn: "FraccionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
