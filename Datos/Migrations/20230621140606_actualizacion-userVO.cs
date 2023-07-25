using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionuserVO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "Email_Valor");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuarios",
                newName: "Contraseña_Valor");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                newName: "IX_Usuarios_Email_Valor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email_Valor",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Contraseña_Valor",
                table: "Usuarios",
                newName: "Contraseña");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email_Valor",
                table: "Usuarios",
                newName: "IX_Usuarios_Email");
        }
    }
}
