using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Usuario.UserCases
{
    public class CU_ConvertUserToDTO:IConvertUserToDTO
    {
        public UsuarioDTO ToUsuarioDTO(Usuario usuario)
        {
            if (usuario != null)
            {
                return new UsuarioDTO()
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre.Valor,
                    Apellido = usuario.Apellido.Valor,
                    Email = usuario.Email.Valor,
                    Contraseña = usuario.Contraseña.Valor,
                    Rol = usuario.Rol.Valor,

                };
            }
            else
            {
                return null;
            }
        }

    }
}
