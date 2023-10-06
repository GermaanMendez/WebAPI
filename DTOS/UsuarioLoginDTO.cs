using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class UsuarioLoginDTO
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
    }
}
