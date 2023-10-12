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
    public class CU_GetUsuarioByEmail : IGetUsuarioByEmail
    {
        IRepositorioUsuario RepositorioUsuario { get; set; }
        public CU_GetUsuarioByEmail(IRepositorioUsuario repo)
        {
            RepositorioUsuario = repo;
        }
        public UsuarioDTO GetUsuarioByEmail(string email)
        {
            Usuario resultado = RepositorioUsuario.GetUsuarioByEmail(email);
            UsuarioDTO usuarioDto = new UsuarioDTO()
            {
                Id = resultado.Id,
                Email = resultado.Email.Valor,
                Contraseña = resultado.Contraseña.Valor,
                Nombre = resultado.Nombre.Valor,
                Apellido = resultado.Nombre.Valor,
                Rol = resultado.Rol.Valor

            };
            return usuarioDto;
        }
    }
}
