using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mensajeria_Linux.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administradores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administradores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Agencias",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombreAgencia = table.Column<string>(type: "text", nullable: false),
                    AdminsitradorId = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    puedeEmail = table.Column<bool>(type: "boolean", nullable: false),
                    puedeTeams = table.Column<bool>(type: "boolean", nullable: false),
                    puedeSMS = table.Column<bool>(type: "boolean", nullable: false),
                    puedeWhatsApp = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencias", x => x.id);
                    table.ForeignKey(
                        name: "FK_Agencias_administradores_AdminsitradorId",
                        column: x => x.AdminsitradorId,
                        principalTable: "administradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "infoEmail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    host = table.Column<string>(type: "text", nullable: false),
                    port = table.Column<int>(type: "integer", nullable: false),
                    emailOrigen = table.Column<string>(type: "text", nullable: false),
                    emailTokenPassword = table.Column<string>(type: "text", nullable: false),
                    emailNombre = table.Column<string>(type: "text", nullable: false),
                    agenciaId = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infoEmail", x => x.id);
                    table.ForeignKey(
                        name: "FK_infoEmail_Agencias_agenciaId",
                        column: x => x.agenciaId,
                        principalTable: "Agencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "infoSMS",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    awsAcceskey = table.Column<string>(type: "text", nullable: false),
                    awsSecretKey = table.Column<string>(type: "text", nullable: false),
                    agenciaId = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infoSMS", x => x.id);
                    table.ForeignKey(
                        name: "FK_infoSMS_Agencias_agenciaId",
                        column: x => x.agenciaId,
                        principalTable: "Agencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "infoTeams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    webHook = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    agenciaId = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infoTeams", x => x.id);
                    table.ForeignKey(
                        name: "FK_infoTeams_Agencias_agenciaId",
                        column: x => x.agenciaId,
                        principalTable: "Agencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "infoWhatsApp",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "text", nullable: false),
                    idioma = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    agenciaId = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infoWhatsApp", x => x.id);
                    table.ForeignKey(
                        name: "FK_infoWhatsApp_Agencias_agenciaId",
                        column: x => x.agenciaId,
                        principalTable: "Agencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plantilla",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    plantillaHtml = table.Column<string>(type: "text", nullable: false),
                    plantillaJSON = table.Column<byte[]>(type: "bytea", nullable: false),
                    plantillaPlana = table.Column<string>(type: "text", nullable: false),
                    agenciaId = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    update = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantilla", x => x.id);
                    table.ForeignKey(
                        name: "FK_Plantilla_Agencias_agenciaId",
                        column: x => x.agenciaId,
                        principalTable: "Agencias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agencias_AdminsitradorId",
                table: "Agencias",
                column: "AdminsitradorId");

            migrationBuilder.CreateIndex(
                name: "IX_infoEmail_agenciaId",
                table: "infoEmail",
                column: "agenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_infoSMS_agenciaId",
                table: "infoSMS",
                column: "agenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_infoTeams_agenciaId",
                table: "infoTeams",
                column: "agenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_infoWhatsApp_agenciaId",
                table: "infoWhatsApp",
                column: "agenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantilla_agenciaId",
                table: "Plantilla",
                column: "agenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "infoEmail");

            migrationBuilder.DropTable(
                name: "infoSMS");

            migrationBuilder.DropTable(
                name: "infoTeams");

            migrationBuilder.DropTable(
                name: "infoWhatsApp");

            migrationBuilder.DropTable(
                name: "Plantilla");

            migrationBuilder.DropTable(
                name: "Agencias");

            migrationBuilder.DropTable(
                name: "administradores");
        }
    }
}
