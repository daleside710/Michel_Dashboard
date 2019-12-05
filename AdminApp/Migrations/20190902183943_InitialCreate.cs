using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "general_gen",
                columns: table => new
                {
                    id_gen = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    activa_gen = table.Column<int>(nullable: true),
                    alertacookies_gen = table.Column<string>(type: "text", nullable: true),
                    analytics_gen = table.Column<string>(type: "text", nullable: true),
                    chckcine_gen = table.Column<int>(nullable: true),
                    chckcookie_gen = table.Column<int>(nullable: true),
                    chckdni_gen = table.Column<int>(nullable: true),
                    chckemail_gen = table.Column<int>(nullable: true),
                    chcknumfac_gen = table.Column<int>(nullable: true),
                    chckpremios_gen = table.Column<int>(nullable: true),
                    chcktotalpremios_gen = table.Column<int>(nullable: true),
                    contacto_gen = table.Column<string>(type: "text", nullable: true),
                    email_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    emailaviso_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    emailavisochk_gen = table.Column<int>(nullable: true),
                    enviadoemailaviso_gen = table.Column<int>(nullable: true),
                    fechaM_gen = table.Column<DateTime>(nullable: true),
                    fechadesde_gen = table.Column<DateTime>(type: "Date", nullable: true),
                    fechahasta_gen = table.Column<DateTime>(type: "Date", nullable: true),
                    filePDF01_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    filecookie_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    filelegal_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    fileprivacidad_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    mecanica_gen = table.Column<string>(type: "text", nullable: true),
                    nombre_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    numemailaviso_gen = table.Column<int>(nullable: true),
                    nummaxcookie_gen = table.Column<int>(nullable: true),
                    nummaxdni_gen = table.Column<int>(nullable: true),
                    nummaxemail_gen = table.Column<int>(nullable: true),
                    nummaxnumfac_gen = table.Column<int>(nullable: true),
                    nummaxpremios_gen = table.Column<int>(nullable: true),
                    numtotalpremios_gen = table.Column<int>(nullable: true),
                    telefono_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    token_gen = table.Column<string>(type: "varchar(255)", nullable: true),
                    usuM_gen = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_gen", x => x.id_gen);
                });

            migrationBuilder.CreateTable(
                name: "llantas_model",
                columns: table => new
                {
                    id_marca = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    literal_marca = table.Column<string>(type: "varchar(255)", nullable: true),
                    orden_marca = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_llantas_model", x => x.id_marca);
                });

            migrationBuilder.CreateTable(
                name: "participaciones_par",
                columns: table => new
                {
                    id_par = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExisteAdjunto = table.Column<string>(type: "varchar(10)", nullable: true),
                    ExisteAdjunto2 = table.Column<string>(type: "varchar(10)", nullable: true),
                    IP_Usuario = table.Column<string>(type: "varchar(50)", nullable: true),
                    Numero_ruedas = table.Column<string>(type: "varchar(255)", nullable: true),
                    Numero_ruedas_int = table.Column<int>(nullable: true),
                    PremioseleccionadoBackp = table.Column<string>(type: "varchar(255)", nullable: true),
                    TamanoRueda = table.Column<string>(type: "varchar(255)", nullable: true),
                    adjunto1_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    adjunto2_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    adjunto3_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    adjunto4_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    adjunto5_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    adjunto_adjunto = table.Column<int>(nullable: true),
                    apellidos_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    codigo2_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    codigo3_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    codigo4_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    codigo5_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    codigo_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    codigopostal_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    comentarios_par = table.Column<string>(type: "text", nullable: true),
                    comercial_par = table.Column<int>(nullable: true),
                    direccion_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    direcciontaller_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    distribuidor_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    dni_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    dondeconociste_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    edad_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    email_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    entregafecha_par = table.Column<DateTime>(type: "Date", nullable: true),
                    entregahora_par = table.Column<TimeSpan>(nullable: true),
                    enviadoFechaRegaloFisico = table.Column<DateTime>(nullable: true),
                    enviadoRegaloFisico = table.Column<int>(nullable: true),
                    factura1_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    factura2_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    factura3_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    factura4_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    factura5_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    fechaCompra1_par = table.Column<DateTime>(type: "Date", nullable: true),
                    fechaCompra2_par = table.Column<DateTime>(type: "Date", nullable: true),
                    fechaCompra3_par = table.Column<DateTime>(type: "Date", nullable: true),
                    fechaCompra4_par = table.Column<DateTime>(type: "Date", nullable: true),
                    fechaCompra5_par = table.Column<DateTime>(type: "Date", nullable: true),
                    fechaEnvioEmail_par = table.Column<DateTime>(nullable: true),
                    fechaM_par = table.Column<DateTime>(nullable: true),
                    fechaValidacion_par = table.Column<DateTime>(nullable: true),
                    fecha_hora_adjunto_adjunto = table.Column<DateTime>(nullable: true),
                    fecha_hora_solicitar_adjunto = table.Column<DateTime>(nullable: true),
                    fechaid_gan = table.Column<DateTime>(type: "Date", nullable: true),
                    id_est = table.Column<int>(nullable: true),
                    id_gan = table.Column<int>(nullable: false),
                    id_ganbck = table.Column<int>(nullable: true),
                    id_tall = table.Column<int>(nullable: true),
                    llanta = table.Column<string>(type: "varchar(255)", nullable: true),
                    localidad_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    localizador_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    lugar_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    medidallanta_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    motivo_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    nacimiento_par = table.Column<DateTime>(type: "Date", nullable: true),
                    nacionalidad_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    nombre_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    nombreapellidos_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    nombretaller_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    numllantas_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    oleada_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    pais_par = table.Column<string>(type: "varchar(2)", nullable: true),
                    premioSelBck_par = table.Column<int>(nullable: true),
                    premioSelFrnt_par = table.Column<int>(nullable: false),
                    provincia_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    rechazoFecha_par = table.Column<DateTime>(type: "Date", nullable: true),
                    rechazoHora_par = table.Column<TimeSpan>(nullable: true),
                    registrocaducidadfecha_par = table.Column<DateTime>(type: "Date", nullable: true),
                    registrocaducidadhora_par = table.Column<TimeSpan>(nullable: true),
                    registrofechaOK_par = table.Column<DateTime>(type: "Date", nullable: true),
                    registrofecha_par = table.Column<DateTime>(type: "Date", nullable: true),
                    registrohora_par = table.Column<TimeSpan>(nullable: true),
                    sexo_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    solicitar_adjunto = table.Column<int>(nullable: true),
                    talla_par = table.Column<string>(type: "varchar(50)", nullable: true),
                    telefono_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    ticket_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    tipovehiculo_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    token = table.Column<string>(type: "varchar(255)", nullable: true),
                    url_par = table.Column<string>(type: "varchar(255)", nullable: true),
                    usuEnvioEmail_par = table.Column<int>(nullable: true),
                    usuM_par = table.Column<int>(nullable: true),
                    usuValidacion_par = table.Column<int>(nullable: true),
                    valoracion1_par = table.Column<int>(nullable: true),
                    valoracion2_par = table.Column<int>(nullable: true),
                    valoracion3_par = table.Column<int>(nullable: true),
                    valoracion4_par = table.Column<int>(nullable: true),
                    valoracion5_par = table.Column<int>(nullable: true),
                    valorpremio_par = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participaciones_par", x => x.id_par);
                    table.UniqueConstraint("AK_participaciones_par_premioSelFrnt_par", x => x.premioSelFrnt_par);
                });

            migrationBuilder.CreateTable(
                name: "regalosalert_ale",
                columns: table => new
                {
                    id_ale = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isGrupo_ale = table.Column<int>(nullable: false),
                    producto_ale = table.Column<string>(type: "varchar(255)", nullable: true),
                    stock_ale = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regalosalert_ale", x => x.id_ale);
                });

            migrationBuilder.CreateTable(
                name: "regaloslimite_lim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    especial = table.Column<string>(type: "varchar(100)", nullable: true),
                    grupoproducto_lim = table.Column<string>(type: "varchar(255)", nullable: true),
                    unidades_lim = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regaloslimite_lim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_usu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    email_usu = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    pass_usu = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    activo_usu = table.Column<int>(nullable: false),
                    apellidos_usu = table.Column<string>(maxLength: 255, nullable: true),
                    borrado_usu = table.Column<int>(nullable: false),
                    fechaC_usu = table.Column<DateTime>(nullable: true),
                    fechaM_usu = table.Column<DateTime>(nullable: true),
                    fechaUltAcceso_usu = table.Column<DateTime>(nullable: true),
                    nombre_usu = table.Column<string>(maxLength: 255, nullable: true),
                    pais_usu = table.Column<string>(maxLength: 255, nullable: true),
                    tipo_usu = table.Column<string>(maxLength: 255, nullable: true),
                    usuC_usu = table.Column<int>(nullable: false),
                    usuM_usu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_usu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "talleres_tall",
                columns: table => new
                {
                    id_tall = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ENSENA_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    HPDV_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    LC_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    REGION_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    alias_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    cp_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    direccion_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    email_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    fechaC_tall = table.Column<DateTime>(type: "DateTime", nullable: true),
                    fechaDesde_tall = table.Column<DateTime>(type: "DateTime", nullable: true),
                    fechaHasta_tall = table.Column<DateTime>(type: "DateTime", nullable: true),
                    fechaM_tall = table.Column<DateTime>(type: "DateTime", nullable: true),
                    pais_tall = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    poblacion_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    provincia_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    razonsocial_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    telefono_tall = table.Column<string>(type: "varchar(255)", nullable: true),
                    usuC_tall = table.Column<int>(nullable: true),
                    usuM_tall = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_talleres_tall", x => x.id_tall);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
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
                name: "regalos_re",
                columns: table => new
                {
                    id_regalo = table.Column<int>(nullable: false),
                    codigofacturainforme = table.Column<string>(type: "varchar(255)", nullable: true),
                    codigoinforme = table.Column<string>(type: "varchar(255)", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(255)", nullable: true),
                    grupoproducto = table.Column<string>(type: "varchar(255)", nullable: true),
                    link = table.Column<string>(type: "varchar(255)", nullable: true),
                    llantas = table.Column<string>(type: "varchar(255)", nullable: true),
                    mostrar = table.Column<int>(nullable: true),
                    neumaticos = table.Column<string>(type: "varchar(255)", nullable: true),
                    numero_manhattan = table.Column<string>(type: "varchar(250)", nullable: true),
                    pais = table.Column<string>(type: "varchar(255)", nullable: true),
                    paisinforme = table.Column<string>(type: "varchar(255)", nullable: true),
                    plantilla_manhattan = table.Column<string>(type: "varchar(255)", nullable: true),
                    producto = table.Column<string>(type: "varchar(255)", nullable: true),
                    pvpinforme = table.Column<decimal>(nullable: false),
                    tipo = table.Column<string>(type: "varchar(255)", nullable: true),
                    tipo_manhattan = table.Column<string>(type: "varchar(250)", nullable: true),
                    valor = table.Column<string>(type: "varchar(255)", nullable: true),
                    vehiculo = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regalos_re", x => x.id_regalo);
                    table.ForeignKey(
                        name: "FK_regalos_re_participaciones_par_id_regalo",
                        column: x => x.id_regalo,
                        principalTable: "participaciones_par",
                        principalColumn: "premioSelFrnt_par",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_usuarios_usu_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios_usu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_usuarios_usu_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios_usu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
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
                        name: "FK_AspNetUserRoles_usuarios_usu_UserId",
                        column: x => x.UserId,
                        principalTable: "usuarios_usu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_regalos_re_id_regalo",
                table: "regalos_re",
                column: "id_regalo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "usuarios_usu",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "usuarios_usu",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "general_gen");

            migrationBuilder.DropTable(
                name: "llantas_model");

            migrationBuilder.DropTable(
                name: "regalos_re");

            migrationBuilder.DropTable(
                name: "regalosalert_ale");

            migrationBuilder.DropTable(
                name: "regaloslimite_lim");

            migrationBuilder.DropTable(
                name: "talleres_tall");

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
                name: "participaciones_par");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "usuarios_usu");
        }
    }
}
