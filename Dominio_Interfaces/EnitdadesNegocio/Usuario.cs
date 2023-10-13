using Dominio_Interfaces.ExepcionesPropias;
using Dominio_Interfaces.InterfacesDominio;
using Dominio_Interfaces.ValueObjects.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio_Interfaces.EnitdadesNegocio
{
   // [Index(nameof(Email),IsUnique = true)]
    public class Usuario : IValidable
    {
        [Key]
        public int Id { get; set; }
        
        public EmailUsuarioVO Email { get; set; }
        public ContraseñaUsuarioVO Contraseña { get; set; }  
        public NombreUsuarioVO Nombre { get; set; }
        public ApellidoUsuarioVO Apellido { get; set; }
        public RolVO Rol { get; set; }
       
        public void Validar()
        {


        }

    }
}
