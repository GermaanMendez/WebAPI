using CasosUso.CU_Usuario.CUInterfaces;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using Dominio_Interfaces.ValueObjects.Usuario;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Usuario.UserCases
{
    public class CU_ListarCabañasListadasPorDueño:IListarCabañasListadasPorDueño
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public IConvertUserToDTO ConvertUserToDTO { get; set; }

        public CU_ListarCabañasListadasPorDueño(IRepositorioUsuario repo, IConvertUserToDTO covnvert)
        {
            RepoUsuario = repo;
            ConvertUserToDTO = covnvert;
        }

        public IEnumerable<CabañaDTO> UserListedCabins(string email)
        {
            return RepoUsuario.UserListedCabins(email).Select(cab=> new CabañaDTO()
            {
                NumeroHabitacion = cab.NumeroHabitacion,
                Nombre = cab.Nombre.valor,
                Foto = cab.Foto,
                Descripcion = cab.Descripcion.valor,
                PrecioDiario=cab.PrecioPorDia,
                PoseeJacuzzi = cab.PoseeJacuzzi,
                EstaHabilitada = cab.EstaHabilitada,
                CantidadPersonasMax = cab.CantidadPersonasMax,
                IdTipoCabaña = cab.IdTipoCabaña,
                Usuario = ConvertUserToDTO.ToUsuarioDTO(cab.Usuario)
                //IdUsuario = cab.IdUsuario
            });


        }



    }
}
