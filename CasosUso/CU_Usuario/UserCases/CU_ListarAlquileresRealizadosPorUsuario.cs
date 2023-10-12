using CasosUso.CU_Cabaña.InterfacesCU;
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
    public class CU_ListarAlquileresRealizadosPorUsuario : IListarAlquileresRealizadosPorUsuario
    {
        IRepositorioUsuario RepoUsuario{ get; set; }
        IConvertCabañaToDTO ConvertCabañaToDTO { get; set; }
        public CU_ListarAlquileresRealizadosPorUsuario(IRepositorioUsuario repo, IConvertCabañaToDTO convert)
        {
            RepoUsuario = repo;
            ConvertCabañaToDTO = convert;
        }
        public IEnumerable<AlquilerCabañaDTO> ListarAlquileresRealizadosPorUsuario(string emailUsuario)
        {
            return RepoUsuario.ObtenerAlquileresRealizadosPorUsuario(emailUsuario).Select(alq => new AlquilerCabañaDTO()
            {
                Id=alq.IdAlquiler,
                FechaAlquilerDesde = alq.FechaAlquilerDesde,
                FechaAlquilerHasta = alq.FechaAlquilerDesde,
                Precio = alq.Precio,
                UsuarioId = alq.UsuarioId,
                CabañaId = alq.CabañaId,
                Cabaña = ConvertCabañaToDTO.convertCabañaToDTO(alq.Cabaña)
            });
        }
    }
}
