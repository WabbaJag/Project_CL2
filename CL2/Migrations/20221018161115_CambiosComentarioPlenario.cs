using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class CambiosComentarioPlenario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscusionPlenario");

            migrationBuilder.DropTable(
                name: "SesionPlenarioActividad");

            migrationBuilder.DropTable(
                name: "TipoActividadPlenario");

            migrationBuilder.CreateTable(
                name: "ComentarioPlenario",
                columns: table => new
                {
                    ComentarioPlenarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionPlenarioID = table.Column<int>(type: "int", nullable: false),
                    ProyectoLeyID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    TipoActividad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosicionComentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResumenComentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetalleComentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioPlenario", x => x.ComentarioPlenarioID);
                    table.ForeignKey(
                        name: "FK_ComentarioPlenario_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComentarioPlenario_ProyectoLey_ProyectoLeyID",
                        column: x => x.ProyectoLeyID,
                        principalTable: "ProyectoLey",
                        principalColumn: "ProyectoLeyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComentarioPlenario_SesionPlenario_SesionPlenarioID",
                        column: x => x.SesionPlenarioID,
                        principalTable: "SesionPlenario",
                        principalColumn: "SesionPlenarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioPlenario_DiputadoID",
                table: "ComentarioPlenario",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioPlenario_ProyectoLeyID",
                table: "ComentarioPlenario",
                column: "ProyectoLeyID");

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioPlenario_SesionPlenarioID",
                table: "ComentarioPlenario",
                column: "SesionPlenarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentarioPlenario");

            migrationBuilder.CreateTable(
                name: "TipoActividadPlenario",
                columns: table => new
                {
                    TipoActividadPlenarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetalleActividadPlenario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActividadPlenario", x => x.TipoActividadPlenarioID);
                });

            migrationBuilder.CreateTable(
                name: "SesionPlenarioActividad",
                columns: table => new
                {
                    SesionPlenarioID = table.Column<int>(type: "int", nullable: false),
                    SesionPlenarioActividadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DecisionMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetalleActividadPlenario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    NumeroMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProyectoLeyID = table.Column<int>(type: "int", nullable: false),
                    TipoActividadPlenarioID = table.Column<int>(type: "int", nullable: false),
                    TipoDebate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VotosContraDebate = table.Column<int>(type: "int", nullable: false),
                    VotosFavorDebate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionPlenarioActividad", x => new { x.SesionPlenarioID, x.SesionPlenarioActividadID });
                    table.ForeignKey(
                        name: "FK_SesionPlenarioActividad_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionPlenarioActividad_ProyectoLey_ProyectoLeyID",
                        column: x => x.ProyectoLeyID,
                        principalTable: "ProyectoLey",
                        principalColumn: "ProyectoLeyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionPlenarioActividad_SesionPlenario_SesionPlenarioID",
                        column: x => x.SesionPlenarioID,
                        principalTable: "SesionPlenario",
                        principalColumn: "SesionPlenarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionPlenarioActividad_TipoActividadPlenario_TipoActividadPlenarioID",
                        column: x => x.TipoActividadPlenarioID,
                        principalTable: "TipoActividadPlenario",
                        principalColumn: "TipoActividadPlenarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscusionPlenario",
                columns: table => new
                {
                    DiscusionPlenarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionPlenarioID = table.Column<int>(type: "int", nullable: false),
                    SesionPlenarioActividadID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    DiscusionDetalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscusionResumen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoComentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscusionPlenario", x => new { x.DiscusionPlenarioID, x.SesionPlenarioID, x.SesionPlenarioActividadID });
                    table.ForeignKey(
                        name: "FK_DiscusionPlenario_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscusionPlenario_SesionPlenarioActividad_SesionPlenarioID_SesionPlenarioActividadID",
                        columns: x => new { x.SesionPlenarioID, x.SesionPlenarioActividadID },
                        principalTable: "SesionPlenarioActividad",
                        principalColumns: new[] { "SesionPlenarioID", "SesionPlenarioActividadID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscusionPlenario_DiputadoID",
                table: "DiscusionPlenario",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_DiscusionPlenario_SesionPlenarioID_SesionPlenarioActividadID",
                table: "DiscusionPlenario",
                columns: new[] { "SesionPlenarioID", "SesionPlenarioActividadID" });

            migrationBuilder.CreateIndex(
                name: "IX_SesionPlenarioActividad_DiputadoID",
                table: "SesionPlenarioActividad",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionPlenarioActividad_ProyectoLeyID",
                table: "SesionPlenarioActividad",
                column: "ProyectoLeyID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionPlenarioActividad_TipoActividadPlenarioID",
                table: "SesionPlenarioActividad",
                column: "TipoActividadPlenarioID");
        }
    }
}
