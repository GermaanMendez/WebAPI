using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ValueObjects.Cabaña;
using Dominio_Interfaces.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set;}
        public string Rol { get; set; }

        public Usuario ToUsuario()
        {
            return new Usuario()
            {
                Id = this.Id,
                Nombre = new NombreUsuarioVO(this.Nombre),
                Apellido = new ApellidoUsuarioVO(this.Apellido),
                Email = new EmailUsuarioVO(this.Email),
                Contraseña = new ContraseñaUsuarioVO(this.Contraseña),
                Rol = new RolVO(this.Rol),
            };
        }
    }
}
