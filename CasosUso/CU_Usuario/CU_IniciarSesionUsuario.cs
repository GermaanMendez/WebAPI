using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Usuario
{
    public class CU_IniciarSesionUsuario : I_iniciarSesionUsuario
    {
        IRepositorioUsuario repoUsuario { get; set; }
        public CU_IniciarSesionUsuario(IRepositorioUsuario repo)
        {
            repoUsuario = repo;
        }
        public UsuarioDTO IniciarSesionUsuario(string mail, string Contraseña)
        {
            Usuario usu = repoUsuario.IniciarSesion(mail, Contraseña);

            if(usu != null)
            {
                UsuarioDTO usuarioDTO = new UsuarioDTO()
                {
                    Email=usu.Email.Valor,
                    Contraseña = usu.Contraseña.Valor
                };
                return usuarioDTO;
            }
            else
            {
                return null;
            }

        }
    }
}
