using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class IntegrantesPlenarioCambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario",
                column: "FraccionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario",
                column: "FraccionID",
                unique: true,
                filter: "[FraccionID] IS NOT NULL");
        }
    }
}
