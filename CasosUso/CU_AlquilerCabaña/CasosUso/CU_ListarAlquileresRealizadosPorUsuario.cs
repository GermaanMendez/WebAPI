using CasosUso.CU_AlquilerCabaña.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_AlquilerCabaña.CasosUso
{
    public class CU_ListarAlquileresRealizadosPorUsuario : IListarAlquileresRealizadosPorUsuario
    {
        IRepositorioAlquilerCabaña RepoAlquiler { get; set; }
        public CU_ListarAlquileresRealizadosPorUsuario(IRepositorioAlquilerCabaña repo)
        {
            RepoAlquiler = repo;   
        }
        public IEnumerable<AlquilerCabañaDTO> ListarAlquileresRealizadosPorUsuario(string emailUsuario)
        {
            return RepoAlquiler.ObtenerAlquileresRealizadosPorUsuario(emailUsuario).Select(alq => new AlquilerCabañaDTO()
            {
                FechaAlquilerDesde = alq.FechaAlquilerDesde,
                FechaAlquilerHasta = alq.FechaAlquilerDesde,
                Precio = alq.Precio,
                UsuarioId = alq.UsuarioId,
                CabañaId = alq.CabañaId,
                Cabaña=alq.Cabaña
            });
        }
    }
}
