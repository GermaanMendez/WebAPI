using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class Creacionbase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreParametro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposCabañas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCabañas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email_Valor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contraseña_Valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre_Valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido_Valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol_Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabañas",
                columns: table => new
                {
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_valor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion_valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoseeJacuzzi = table.Column<bool>(type: "bit", nullable: false),
                    EstaHabilitada = table.Column<bool>(type: "bit", nullable: false),
                    CantidadPersonasMax = table.Column<int>(type: "int", nullable: false),
                    PrecioPorDia = table.Column<int>(type: "int", nullable: false),
                    IdTipoCabaña = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabañas", x => x.NumeroHabitacion);
                    table.ForeignKey(
                        name: "FK_Cabañas_TiposCabañas_IdTipoCabaña",
                        column: x => x.IdTipoCabaña,
                        principalTable: "TiposCabañas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cabañas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlquileresCabañas",
                columns: table => new
                {
                    IdAlquiler = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAlquilerDesde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaAlquilerHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    CabañaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlquileresCabañas", x => x.IdAlquiler);
                    table.ForeignKey(
                        name: "FK_AlquileresCabañas_Cabañas_CabañaId",
                        column: x => x.CabañaId,
                        principalTable: "Cabañas",
                        principalColumn: "NumeroHabitacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlquileresCabañas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaMantenimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CostoMantenimiento = table.Column<double>(type: "float", nullable: false),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IdCabaña = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Cabañas_IdCabaña",
                        column: x => x.IdCabaña,
                        principalTable: "Cabañas",
                        principalColumn: "NumeroHabitacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlquileresCabañas_CabañaId",
                table: "AlquileresCabañas",
                column: "CabañaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlquileresCabañas_UsuarioId",
                table: "AlquileresCabañas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cabañas_IdTipoCabaña",
                table: "Cabañas",
                column: "IdTipoCabaña");

            migrationBuilder.CreateIndex(
                name: "IX_Cabañas_Nombre_valor",
                table: "Cabañas",
                column: "Nombre_valor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabañas_UsuarioId",
                table: "Cabañas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_IdCabaña",
                table: "Mantenimientos",
                column: "IdCabaña");

            migrationBuilder.CreateIndex(
                name: "IX_TiposCabañas_Nombre",
                table: "TiposCabañas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email_Valor",
                table: "Usuarios",
                column: "Email_Valor",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlquileresCabañas");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Cabañas");

            migrationBuilder.DropTable(
                name: "TiposCabañas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
