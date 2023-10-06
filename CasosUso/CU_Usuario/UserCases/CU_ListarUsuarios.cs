using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Usuario.UserCases
{
    public class CU_ListarUsuarios : I_ListarUsuarios
    {
        IRepositorioUsuario repoUsuario { get; set; }
        public CU_ListarUsuarios(IRepositorioUsuario repo)
        {
            repoUsuario = repo;
        }

        public IEnumerable<UsuarioDTO> GetUsuarios()
        {
            return repoUsuario.GetAllUsuarios().Select(usuario => new UsuarioDTO()
            {
                Id = usuario.Id,
                Email = usuario.Email.Valor,
                Contraseña = usuario.Contraseña.Valor,
                Nombre = usuario.Nombre.Valor,
                Apellido = usuario.Apellido.Valor,
                Rol = usuario.Rol.Valor
            });
        }
    }
}
