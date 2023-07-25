using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class CreacionBaseObligatorio : Migration
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
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoPorHuesped = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposCabañas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cabañas",
                columns: table => new
                {
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoseeJacuzzi = table.Column<bool>(type: "bit", nullable: false),
                    EstaHabilitada = table.Column<bool>(type: "bit", nullable: false),
                    CantidadPersonasMax = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoCabaña = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaMantenimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoMantenimiento = table.Column<double>(type: "float", nullable: false),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "IX_Cabañas_IdTipoCabaña",
                table: "Cabañas",
                column: "IdTipoCabaña");

            migrationBuilder.CreateIndex(
                name: "IX_Cabañas_Nombre",
                table: "Cabañas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_IdCabaña",
                table: "Mantenimientos",
                column: "IdCabaña");

            migrationBuilder.CreateIndex(
                name: "IX_TiposCabañas_Nombre",
                table: "TiposCabañas",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Cabañas");

            migrationBuilder.DropTable(
                name: "TiposCabañas");
        }
    }
}
