using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CL2.Migrations
{
    public partial class RESET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administracion",
                columns: table => new
                {
                    AdministracionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdministracionPeriodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresidenteRepublica = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administracion", x => x.AdministracionID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diputado",
                columns: table => new
                {
                    DiputadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDiputado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoDiputado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoDiputado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneroDiputado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diputado", x => x.DiputadoID);
                });

            migrationBuilder.CreateTable(
                name: "Fraccion",
                columns: table => new
                {
                    FraccionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreFraccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiglasFraccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fraccion", x => x.FraccionID);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoLey",
                columns: table => new
                {
                    ProyectoLeyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroExpediente = table.Column<int>(type: "int", nullable: false),
                    TituloCorto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TituloCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPresentacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadFirmas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoLey", x => x.ProyectoLeyID);
                });

            migrationBuilder.CreateTable(
                name: "Tema",
                columns: table => new
                {
                    TemaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemaDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tema", x => x.TemaID);
                });

            migrationBuilder.CreateTable(
                name: "TipoActividadComision",
                columns: table => new
                {
                    TipoActividadComisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetalleActividadComision = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActividadComision", x => x.TipoActividadComisionID);
                });

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
                name: "Legislativo",
                columns: table => new
                {
                    LegislativoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnoLegislativo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministracionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legislativo", x => x.LegislativoID);
                    table.ForeignKey(
                        name: "FK_Legislativo_Administracion_AdministracionID",
                        column: x => x.AdministracionID,
                        principalTable: "Administracion",
                        principalColumn: "AdministracionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plenario",
                columns: table => new
                {
                    PlenarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdministracionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plenario", x => x.PlenarioID);
                    table.ForeignKey(
                        name: "FK_Plenario_Administracion_AdministracionID",
                        column: x => x.AdministracionID,
                        principalTable: "Administracion",
                        principalColumn: "AdministracionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoDiputado",
                columns: table => new
                {
                    ProyectoLeyID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoDiputado", x => new { x.ProyectoLeyID, x.DiputadoID });
                    table.ForeignKey(
                        name: "FK_ProyectoDiputado_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectoDiputado_ProyectoLey_ProyectoLeyID",
                        column: x => x.ProyectoLeyID,
                        principalTable: "ProyectoLey",
                        principalColumn: "ProyectoLeyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProyectoTema",
                columns: table => new
                {
                    ProyectoLeyID = table.Column<int>(type: "int", nullable: false),
                    TemaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoTema", x => new { x.ProyectoLeyID, x.TemaID });
                    table.ForeignKey(
                        name: "FK_ProyectoTema_ProyectoLey_ProyectoLeyID",
                        column: x => x.ProyectoLeyID,
                        principalTable: "ProyectoLey",
                        principalColumn: "ProyectoLeyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectoTema_Tema_TemaID",
                        column: x => x.TemaID,
                        principalTable: "Tema",
                        principalColumn: "TemaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegrantesPlenario",
                columns: table => new
                {
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    FraccionID = table.Column<int>(type: "int", nullable: false),
                    PlenarioID = table.Column<int>(type: "int", nullable: false),
                    Detalle_Puesto = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrantesPlenario", x => new { x.PlenarioID, x.DiputadoID });
                    table.ForeignKey(
                        name: "FK_IntegrantesPlenario_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegrantesPlenario_Plenario_PlenarioID",
                        column: x => x.PlenarioID,
                        principalTable: "Plenario",
                        principalColumn: "PlenarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegrantesPlenario_Fraccion_FraccionID",
                        column: x => x.FraccionID,
                        principalTable: "Fraccion",
                        principalColumn: "FraccionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionPlenario",
                columns: table => new
                {
                    SesionPlenarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlenarioID = table.Column<int>(type: "int", nullable: false),
                    NumeroSesion = table.Column<int>(type: "int", nullable: false),
                    PeriodoSesionPlenario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoSesionPlenario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSesionPlenario = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionPlenario", x => x.SesionPlenarioID);
                    table.ForeignKey(
                        name: "FK_SesionPlenario_Plenario_PlenarioID",
                        column: x => x.PlenarioID,
                        principalTable: "Plenario",
                        principalColumn: "PlenarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlPolitico",
                columns: table => new
                {
                    ControlPoliticoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionPlenarioID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    TemaID = table.Column<int>(type: "int", nullable: false),
                    ResumenComentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetalleComentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPolitico", x => new { x.SesionPlenarioID, x.ControlPoliticoID });
                    table.ForeignKey(
                        name: "FK_ControlPolitico_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlPolitico_SesionPlenario_SesionPlenarioID",
                        column: x => x.SesionPlenarioID,
                        principalTable: "SesionPlenario",
                        principalColumn: "SesionPlenarioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlPolitico_Tema_TemaID",
                        column: x => x.TemaID,
                        principalTable: "Tema",
                        principalColumn: "TemaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionPlenarioActividad",
                columns: table => new
                {
                    SesionPlenarioActividadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionPlenarioID = table.Column<int>(type: "int", nullable: false),
                    TipoActividadPlenarioID = table.Column<int>(type: "int", nullable: false),
                    ProyectoLeyID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    NumeroMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDebate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VotosFavorDebate = table.Column<int>(type: "int", nullable: false),
                    VotosContraDebate = table.Column<int>(type: "int", nullable: false),
                    DetalleActividadPlenario = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    DiscusionResumen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscusionDetalle = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ComisionLegislativo",
                columns: table => new
                {
                    ComisionLegislativoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComisionID = table.Column<int>(type: "int", nullable: false),
                    LegislativoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComisionLegislativo", x => x.ComisionLegislativoID);
                    table.ForeignKey(
                        name: "FK_ComisionLegislativo_Legislativo_LegislativoID",
                        column: x => x.LegislativoID,
                        principalTable: "Legislativo",
                        principalColumn: "LegislativoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComisionDiputados",
                columns: table => new
                {
                    ComisionLegislativoID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    DetallePuesto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComisionDiputados", x => new { x.ComisionLegislativoID, x.DiputadoID });
                    table.ForeignKey(
                        name: "FK_ComisionDiputados_ComisionLegislativo_ComisionLegislativoID",
                        column: x => x.ComisionLegislativoID,
                        principalTable: "ComisionLegislativo",
                        principalColumn: "ComisionLegislativoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComisionDiputados_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionComision",
                columns: table => new
                {
                    SesionComisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComisionLegislativoID = table.Column<int>(type: "int", nullable: false),
                    NumeroSesionComision = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PeriodoSesionComision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSesionComision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoSesionComision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegislativoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionComision", x => x.SesionComisionID);
                    table.ForeignKey(
                        name: "FK_SesionComision_ComisionLegislativo_ComisionLegislativoID",
                        column: x => x.ComisionLegislativoID,
                        principalTable: "ComisionLegislativo",
                        principalColumn: "ComisionLegislativoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionComision_Legislativo_LegislativoID",
                        column: x => x.LegislativoID,
                        principalTable: "Legislativo",
                        principalColumn: "LegislativoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comision",
                columns: table => new
                {
                    ComisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreComision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemaID = table.Column<int>(type: "int", nullable: false),
                    SesionComisionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comision", x => x.ComisionID);
                    table.ForeignKey(
                        name: "FK_Comision_SesionComision_SesionComisionID",
                        column: x => x.SesionComisionID,
                        principalTable: "SesionComision",
                        principalColumn: "SesionComisionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comision_Tema_TemaID",
                        column: x => x.TemaID,
                        principalTable: "Tema",
                        principalColumn: "TemaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SesionComisionActividad",
                columns: table => new
                {
                    SesionComisionActividadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionComisionID = table.Column<int>(type: "int", nullable: false),
                    ProyectoLeyID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    TipoActividadID = table.Column<int>(type: "int", nullable: false),
                    NumeroMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionMocion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VotosFavorDictamen = table.Column<int>(type: "int", nullable: false),
                    VotosContraDictamen = table.Column<int>(type: "int", nullable: false),
                    DetalleActividad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoActividadComisionesTipoActividadComisionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionComisionActividad", x => new { x.SesionComisionActividadID, x.SesionComisionID });
                    table.ForeignKey(
                        name: "FK_SesionComisionActividad_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionComisionActividad_ProyectoLey_ProyectoLeyID",
                        column: x => x.ProyectoLeyID,
                        principalTable: "ProyectoLey",
                        principalColumn: "ProyectoLeyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionComisionActividad_SesionComision_SesionComisionID",
                        column: x => x.SesionComisionID,
                        principalTable: "SesionComision",
                        principalColumn: "SesionComisionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionComisionActividad_TipoActividadComision_TipoActividadComisionesTipoActividadComisionID",
                        column: x => x.TipoActividadComisionesTipoActividadComisionID,
                        principalTable: "TipoActividadComision",
                        principalColumn: "TipoActividadComisionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscusionComision",
                columns: table => new
                {
                    DiscusionComisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SesionComisionID = table.Column<int>(type: "int", nullable: false),
                    SesionComisionActividadID = table.Column<int>(type: "int", nullable: false),
                    DiputadoID = table.Column<int>(type: "int", nullable: false),
                    ResumenComentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetalleComentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoComentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscusionComision", x => new { x.DiscusionComisionID, x.SesionComisionActividadID, x.SesionComisionID });
                    table.ForeignKey(
                        name: "FK_DiscusionComision_Diputado_DiputadoID",
                        column: x => x.DiputadoID,
                        principalTable: "Diputado",
                        principalColumn: "DiputadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscusionComision_SesionComisionActividad_SesionComisionActividadID_SesionComisionID",
                        columns: x => new { x.SesionComisionActividadID, x.SesionComisionID },
                        principalTable: "SesionComisionActividad",
                        principalColumns: new[] { "SesionComisionActividadID", "SesionComisionID" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comision_SesionComisionID",
                table: "Comision",
                column: "SesionComisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Comision_TemaID",
                table: "Comision",
                column: "TemaID");

            migrationBuilder.CreateIndex(
                name: "IX_ComisionDiputados_DiputadoID",
                table: "ComisionDiputados",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_ComisionLegislativo_ComisionID",
                table: "ComisionLegislativo",
                column: "ComisionID");

            migrationBuilder.CreateIndex(
                name: "IX_ComisionLegislativo_LegislativoID",
                table: "ComisionLegislativo",
                column: "LegislativoID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPolitico_DiputadoID",
                table: "ControlPolitico",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPolitico_TemaID",
                table: "ControlPolitico",
                column: "TemaID");

            migrationBuilder.CreateIndex(
                name: "IX_DiscusionComision_DiputadoID",
                table: "DiscusionComision",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_DiscusionComision_SesionComisionActividadID_SesionComisionID",
                table: "DiscusionComision",
                columns: new[] { "SesionComisionActividadID", "SesionComisionID" });

            migrationBuilder.CreateIndex(
                name: "IX_DiscusionPlenario_DiputadoID",
                table: "DiscusionPlenario",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_DiscusionPlenario_SesionPlenarioID_SesionPlenarioActividadID",
                table: "DiscusionPlenario",
                columns: new[] { "SesionPlenarioID", "SesionPlenarioActividadID" });

            migrationBuilder.CreateIndex(
                name: "IX_IntegrantesPlenario_DiputadoID",
                table: "IntegrantesPlenario",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrantesPlenario_FraccionID",
                table: "IntegrantesPlenario",
                column: "FraccionID");

            migrationBuilder.CreateIndex(
                name: "IX_Legislativo_AdministracionID",
                table: "Legislativo",
                column: "AdministracionID");

            migrationBuilder.CreateIndex(
                name: "IX_Plenario_AdministracionID",
                table: "Plenario",
                column: "AdministracionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoDiputado_DiputadoID",
                table: "ProyectoDiputado",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoTema_TemaID",
                table: "ProyectoTema",
                column: "TemaID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionComision_ComisionLegislativoID_NumeroSesionComision",
                table: "SesionComision",
                columns: new[] { "ComisionLegislativoID", "NumeroSesionComision" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SesionComision_LegislativoID",
                table: "SesionComision",
                column: "LegislativoID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionComisionActividad_DiputadoID",
                table: "SesionComisionActividad",
                column: "DiputadoID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionComisionActividad_ProyectoLeyID",
                table: "SesionComisionActividad",
                column: "ProyectoLeyID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionComisionActividad_SesionComisionID",
                table: "SesionComisionActividad",
                column: "SesionComisionID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionComisionActividad_TipoActividadComisionesTipoActividadComisionID",
                table: "SesionComisionActividad",
                column: "TipoActividadComisionesTipoActividadComisionID");

            migrationBuilder.CreateIndex(
                name: "IX_SesionPlenario_PlenarioID_NumeroSesion",
                table: "SesionPlenario",
                columns: new[] { "PlenarioID", "NumeroSesion" },
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ComisionLegislativo_Comision_ComisionID",
                table: "ComisionLegislativo",
                column: "ComisionID",
                principalTable: "Comision",
                principalColumn: "ComisionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comision_SesionComision_SesionComisionID",
                table: "Comision");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ComisionDiputados");

            migrationBuilder.DropTable(
                name: "ControlPolitico");

            migrationBuilder.DropTable(
                name: "DiscusionComision");

            migrationBuilder.DropTable(
                name: "DiscusionPlenario");

            migrationBuilder.DropTable(
                name: "Fraccion");

            migrationBuilder.DropTable(
                name: "IntegrantesPlenario");

            migrationBuilder.DropTable(
                name: "ProyectoDiputado");

            migrationBuilder.DropTable(
                name: "ProyectoTema");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SesionComisionActividad");

            migrationBuilder.DropTable(
                name: "SesionPlenarioActividad");

            migrationBuilder.DropTable(
                name: "TipoActividadComision");

            migrationBuilder.DropTable(
                name: "Diputado");

            migrationBuilder.DropTable(
                name: "ProyectoLey");

            migrationBuilder.DropTable(
                name: "SesionPlenario");

            migrationBuilder.DropTable(
                name: "TipoActividadPlenario");

            migrationBuilder.DropTable(
                name: "Plenario");

            migrationBuilder.DropTable(
                name: "SesionComision");

            migrationBuilder.DropTable(
                name: "ComisionLegislativo");

            migrationBuilder.DropTable(
                name: "Comision");

            migrationBuilder.DropTable(
                name: "Legislativo");

            migrationBuilder.DropTable(
                name: "Tema");

            migrationBuilder.DropTable(
                name: "Administracion");
        }
    }
}
