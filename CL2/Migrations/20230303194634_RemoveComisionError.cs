using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class RemoveComisionError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comision_SesionComision_SesionComisionID",
                table: "Comision");

            migrationBuilder.DropIndex(
                name: "IX_Comision_SesionComisionID",
                table: "Comision");

            migrationBuilder.DropColumn(
                name: "SesionComisionID",
                table: "Comision");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SesionComisionID",
                table: "Comision",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comision_SesionComisionID",
                table: "Comision",
                column: "SesionComisionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comision_SesionComision_SesionComisionID",
                table: "Comision",
                column: "SesionComisionID",
                principalTable: "SesionComision",
                principalColumn: "SesionComisionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
