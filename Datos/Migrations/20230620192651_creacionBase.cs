using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class creacionBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Cabañas",
                newName: "Nombre_valor");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Cabañas",
                newName: "Descripcion_valor");

            migrationBuilder.RenameIndex(
                name: "IX_Cabañas_Nombre",
                table: "Cabañas",
                newName: "IX_Cabañas_Nombre_valor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_valor",
                table: "Cabañas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Descripcion_valor",
                table: "Cabañas",
                newName: "Descripcion");

            migrationBuilder.RenameIndex(
                name: "IX_Cabañas_Nombre_valor",
                table: "Cabañas",
                newName: "IX_Cabañas_Nombre");
        }
    }
}
