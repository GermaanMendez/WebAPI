using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Usuario.UserCases
{
    public class CU_RegistrarUsuario : I_RegistrarUsuario
    {
        IRepositorioUsuario repoUsuario { get; set; }
        public CU_RegistrarUsuario(IRepositorioUsuario repo)
        {
            repoUsuario = repo;
        }

        public UsuarioDTO RegistrarUsuario(UsuarioDTO nuevo)
        {
            Usuario usuarioNuevo = nuevo.ToUsuario();

            Usuario usuarioRegistrado = repoUsuario.RegistrarUsuario(usuarioNuevo);
            if (usuarioRegistrado != null)
            {
                UsuarioDTO usuarioDTRO = new UsuarioDTO()
                {
                    Email = usuarioRegistrado.Email.Valor,
                    Nombre = usuarioRegistrado.Nombre.Valor,
                    Apellido = usuarioRegistrado.Apellido.Valor,
                    Contraseña = usuarioRegistrado.Contraseña.Valor,
                    Rol = usuarioRegistrado.Rol.Valor,
                };
                return usuarioDTRO;
            }
            else
            {
                return null;
            }
        }


    }
}
