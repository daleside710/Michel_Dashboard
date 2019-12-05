using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AdminApp.Data;

namespace AdminApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190902183943_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdminApp.Models.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("AdminApp.Models.General", b =>
                {
                    b.Property<int>("id_gen")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("activa_gen");

                    b.Property<string>("alertacookies_gen")
                        .HasColumnType("text");

                    b.Property<string>("analytics_gen")
                        .HasColumnType("text");

                    b.Property<int?>("chckcine_gen");

                    b.Property<int?>("chckcookie_gen");

                    b.Property<int?>("chckdni_gen");

                    b.Property<int?>("chckemail_gen");

                    b.Property<int?>("chcknumfac_gen");

                    b.Property<int?>("chckpremios_gen");

                    b.Property<int?>("chcktotalpremios_gen");

                    b.Property<string>("contacto_gen")
                        .HasColumnType("text");

                    b.Property<string>("email_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("emailaviso_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("emailavisochk_gen");

                    b.Property<int?>("enviadoemailaviso_gen");

                    b.Property<DateTime?>("fechaM_gen");

                    b.Property<DateTime?>("fechadesde_gen")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("fechahasta_gen")
                        .HasColumnType("Date");

                    b.Property<string>("filePDF01_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("filecookie_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("filelegal_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("fileprivacidad_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("mecanica_gen")
                        .HasColumnType("text");

                    b.Property<string>("nombre_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("numemailaviso_gen");

                    b.Property<int?>("nummaxcookie_gen");

                    b.Property<int?>("nummaxdni_gen");

                    b.Property<int?>("nummaxemail_gen");

                    b.Property<int?>("nummaxnumfac_gen");

                    b.Property<int?>("nummaxpremios_gen");

                    b.Property<int?>("numtotalpremios_gen");

                    b.Property<string>("telefono_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("token_gen")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("usuM_gen");

                    b.HasKey("id_gen");

                    b.ToTable("general_gen");
                });

            modelBuilder.Entity("AdminApp.Models.Llantas", b =>
                {
                    b.Property<int>("id_marca")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("literal_marca")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("orden_marca");

                    b.HasKey("id_marca");

                    b.ToTable("llantas_model");
                });

            modelBuilder.Entity("AdminApp.Models.Participation", b =>
                {
                    b.Property<int>("id_par")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExisteAdjunto")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("ExisteAdjunto2")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("IP_Usuario")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Numero_ruedas")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("Numero_ruedas_int");

                    b.Property<string>("PremioseleccionadoBackp")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TamanoRueda")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("adjunto1_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("adjunto2_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("adjunto3_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("adjunto4_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("adjunto5_par")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("adjunto_adjunto");

                    b.Property<string>("apellidos_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("codigo2_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("codigo3_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("codigo4_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("codigo5_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("codigo_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("codigopostal_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("comentarios_par")
                        .HasColumnType("text");

                    b.Property<int?>("comercial_par");

                    b.Property<string>("direccion_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("direcciontaller_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("distribuidor_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("dni_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("dondeconociste_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("edad_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("email_par")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("entregafecha_par")
                        .HasColumnType("Date");

                    b.Property<TimeSpan?>("entregahora_par");

                    b.Property<DateTime?>("enviadoFechaRegaloFisico");

                    b.Property<int?>("enviadoRegaloFisico");

                    b.Property<string>("factura1_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("factura2_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("factura3_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("factura4_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("factura5_par")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("fechaCompra1_par")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("fechaCompra2_par")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("fechaCompra3_par")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("fechaCompra4_par")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("fechaCompra5_par")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("fechaEnvioEmail_par");

                    b.Property<DateTime?>("fechaM_par");

                    b.Property<DateTime?>("fechaValidacion_par");

                    b.Property<DateTime?>("fecha_hora_adjunto_adjunto");

                    b.Property<DateTime?>("fecha_hora_solicitar_adjunto");

                    b.Property<DateTime?>("fechaid_gan")
                        .HasColumnType("Date");

                    b.Property<int?>("id_est");

                    b.Property<int>("id_gan");

                    b.Property<int?>("id_ganbck");

                    b.Property<int?>("id_tall");

                    b.Property<string>("llanta")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("localidad_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("localizador_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("lugar_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("medidallanta_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("motivo_par")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("nacimiento_par")
                        .HasColumnType("Date");

                    b.Property<string>("nacionalidad_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nombre_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nombreapellidos_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nombretaller_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("numllantas_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("oleada_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("pais_par")
                        .HasColumnType("varchar(2)");

                    b.Property<int?>("premioSelBck_par");

                    b.Property<int?>("premioSelFrnt_par")
                        .IsRequired();

                    b.Property<string>("provincia_par")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("rechazoFecha_par")
                        .HasColumnType("Date");

                    b.Property<TimeSpan?>("rechazoHora_par");

                    b.Property<DateTime?>("registrocaducidadfecha_par")
                        .HasColumnType("Date");

                    b.Property<TimeSpan?>("registrocaducidadhora_par");

                    b.Property<DateTime?>("registrofechaOK_par")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("registrofecha_par")
                        .HasColumnType("Date");

                    b.Property<TimeSpan?>("registrohora_par");

                    b.Property<string>("sexo_par")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("solicitar_adjunto");

                    b.Property<string>("talla_par")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("telefono_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ticket_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("tipovehiculo_par")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("token")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("url_par")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("usuEnvioEmail_par");

                    b.Property<int?>("usuM_par");

                    b.Property<int?>("usuValidacion_par");

                    b.Property<int?>("valoracion1_par");

                    b.Property<int?>("valoracion2_par");

                    b.Property<int?>("valoracion3_par");

                    b.Property<int?>("valoracion4_par");

                    b.Property<int?>("valoracion5_par");

                    b.Property<string>("valorpremio_par")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id_par");

                    b.ToTable("participaciones_par");
                });

            modelBuilder.Entity("AdminApp.Models.Regalo", b =>
                {
                    b.Property<int>("id_regalo");

                    b.Property<string>("codigofacturainforme")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("codigoinforme")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("descripcion")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("grupoproducto")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("link")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("llantas")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("mostrar");

                    b.Property<string>("neumaticos")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("numero_manhattan")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("pais")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("paisinforme")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("plantilla_manhattan")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("producto")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("pvpinforme");

                    b.Property<string>("tipo")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("tipo_manhattan")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("valor")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("vehiculo")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id_regalo");

                    b.HasIndex("id_regalo")
                        .IsUnique();

                    b.ToTable("regalos_re");
                });

            modelBuilder.Entity("AdminApp.Models.Regalosalert", b =>
                {
                    b.Property<int>("id_ale")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("isGrupo_ale");

                    b.Property<string>("producto_ale")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("stock_ale");

                    b.HasKey("id_ale");

                    b.ToTable("regalosalert_ale");
                });

            modelBuilder.Entity("AdminApp.Models.Regaloslimite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("especial")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("grupoproducto_lim")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("unidades_lim");

                    b.HasKey("Id");

                    b.ToTable("regaloslimite_lim");
                });

            modelBuilder.Entity("AdminApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasColumnName("email_usu")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnName("pass_usu");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("activo_usu");

                    b.Property<string>("apellidos_usu")
                        .HasMaxLength(255);

                    b.Property<int>("borrado_usu");

                    b.Property<DateTime?>("fechaC_usu");

                    b.Property<DateTime?>("fechaM_usu");

                    b.Property<DateTime?>("fechaUltAcceso_usu");

                    b.Property<string>("nombre_usu")
                        .HasMaxLength(255);

                    b.Property<string>("pais_usu")
                        .HasMaxLength(255);

                    b.Property<string>("tipo_usu")
                        .HasMaxLength(255);

                    b.Property<int>("usuC_usu");

                    b.Property<int?>("usuM_usu");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.ToTable("usuarios_usu");
                });

            modelBuilder.Entity("AdminApp.Models.Workshop", b =>
                {
                    b.Property<int>("id_tall")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ENSENA_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("HPDV_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LC_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("REGION_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("alias_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("cp_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("direccion_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("email_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("fechaC_tall")
                        .HasColumnType("DateTime");

                    b.Property<DateTime?>("fechaDesde_tall")
                        .HasColumnType("DateTime");

                    b.Property<DateTime?>("fechaHasta_tall")
                        .HasColumnType("DateTime");

                    b.Property<DateTime?>("fechaM_tall")
                        .HasColumnType("DateTime");

                    b.Property<string>("pais_tall")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("poblacion_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("provincia_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("razonsocial_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("telefono_tall")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("usuC_tall");

                    b.Property<int?>("usuM_tall");

                    b.HasKey("id_tall");

                    b.ToTable("talleres_tall");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AdminApp.Models.Regalo", b =>
                {
                    b.HasOne("AdminApp.Models.Participation", "Participation")
                        .WithOne("Regalo")
                        .HasForeignKey("AdminApp.Models.Regalo", "id_regalo")
                        .HasPrincipalKey("AdminApp.Models.Participation", "premioSelFrnt_par")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("AdminApp.Models.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AdminApp.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AdminApp.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("AdminApp.Models.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdminApp.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
