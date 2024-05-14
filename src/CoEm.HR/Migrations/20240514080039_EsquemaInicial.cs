using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoEm.HR.Migrations
{
    /// <inheritdoc />
    public partial class EsquemaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    clave = table.Column<byte[]>(type: "varbinary(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perfil",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    es_empleador = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfil", x => x.id);
                    table.ForeignKey(
                        name: "FK_perfil_usuario_id",
                        column: x => x.id,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "demandante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    fecha_nacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    nivel_educacion = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    experiencia_laboral = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demandante", x => x.id);
                    table.ForeignKey(
                        name: "FK_demandante_perfil_id",
                        column: x => x.id,
                        principalTable: "perfil",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "empleador",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    industria = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    numero_empleados = table.Column<int>(type: "int", nullable: false),
                    ubicacion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleador", x => x.id);
                    table.ForeignKey(
                        name: "FK_empleador_perfil_id",
                        column: x => x.id,
                        principalTable: "perfil",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vacante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empleador_id = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo_contrato = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    salario = table.Column<decimal>(type: "decimal(9,0)", precision: 9, scale: 0, nullable: false),
                    fecha_publicacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacante", x => x.id);
                    table.ForeignKey(
                        name: "FK_vacante_empleador_empleador_id",
                        column: x => x.empleador_id,
                        principalTable: "empleador",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "aplicacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vacante_id = table.Column<int>(type: "int", nullable: false),
                    demandante_id = table.Column<int>(type: "int", nullable: false),
                    fecha_aplicacion = table.Column<DateOnly>(type: "date", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aplicacion", x => x.id);
                    table.ForeignKey(
                        name: "FK_aplicacion_demandante_demandante_id",
                        column: x => x.demandante_id,
                        principalTable: "demandante",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_aplicacion_vacante_vacante_id",
                        column: x => x.vacante_id,
                        principalTable: "vacante",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_aplicacion_demandante_id",
                table: "aplicacion",
                column: "demandante_id");

            migrationBuilder.CreateIndex(
                name: "IX_aplicacion_vacante_id",
                table: "aplicacion",
                column: "vacante_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_email",
                table: "usuario",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vacante_empleador_id",
                table: "vacante",
                column: "empleador_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aplicacion");

            migrationBuilder.DropTable(
                name: "demandante");

            migrationBuilder.DropTable(
                name: "vacante");

            migrationBuilder.DropTable(
                name: "empleador");

            migrationBuilder.DropTable(
                name: "perfil");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
