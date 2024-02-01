using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class CambiosIntegrantesPlenario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntegrantesPlenario_IntegrantesPlenario_IntegrantesPlenarioPlenarioID_IntegrantesPlenarioDiputadoID",
                table: "IntegrantesPlenario");

            migrationBuilder.DropIndex(
                name: "IX_IntegrantesPlenario_IntegrantesPlenarioPlenarioID_IntegrantesPlenarioDiputadoID",
                table: "IntegrantesPlenario");

            migrationBuilder.DropColumn(
                name: "IntegrantesPlenarioDiputadoID",
                table: "IntegrantesPlenario");

            migrationBuilder.DropColumn(
                name: "IntegrantesPlenarioPlenarioID",
                table: "IntegrantesPlenario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntegrantesPlenarioDiputadoID",
                table: "IntegrantesPlenario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntegrantesPlenarioPlenarioID",
                table: "IntegrantesPlenario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntegrantesPlenario_IntegrantesPlenarioPlenarioID_IntegrantesPlenarioDiputadoID",
                table: "IntegrantesPlenario",
                columns: new[] { "IntegrantesPlenarioPlenarioID", "IntegrantesPlenarioDiputadoID" });

            migrationBuilder.AddForeignKey(
                name: "FK_IntegrantesPlenario_IntegrantesPlenario_IntegrantesPlenarioPlenarioID_IntegrantesPlenarioDiputadoID",
                table: "IntegrantesPlenario",
                columns: new[] { "IntegrantesPlenarioPlenarioID", "IntegrantesPlenarioDiputadoID" },
                principalTable: "IntegrantesPlenario",
                principalColumns: new[] { "PlenarioID", "DiputadoID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
