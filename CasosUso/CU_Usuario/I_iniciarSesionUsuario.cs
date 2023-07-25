using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Usuario
{
    public interface I_iniciarSesionUsuario
    {
        UsuarioDTO IniciarSesionUsuario(string mail, string Contraseña);
    }
}
