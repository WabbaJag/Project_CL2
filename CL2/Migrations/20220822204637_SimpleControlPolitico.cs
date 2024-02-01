using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class SimpleControlPolitico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ControlPolitico",
                table: "ControlPolitico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControlPolitico",
                table: "ControlPolitico",
                column: "ControlPoliticoID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPolitico_SesionPlenarioID",
                table: "ControlPolitico",
                column: "SesionPlenarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ControlPolitico",
                table: "ControlPolitico");

            migrationBuilder.DropIndex(
                name: "IX_ControlPolitico_SesionPlenarioID",
                table: "ControlPolitico");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControlPolitico",
                table: "ControlPolitico",
                columns: new[] { "SesionPlenarioID", "ControlPoliticoID" });
        }
    }
}
