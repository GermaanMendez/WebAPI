using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_ListarPorTipo : IListarPorTipo
    {
        IRepositorioCabaña RepoCabaña { get; set; }
        public CU_ListarPorTipo(IRepositorioCabaña repo)
        {
            RepoCabaña = repo;  
        }
        public IEnumerable<CabañaDTO> ListarPorTipo(int idTipo)
        {
            return RepoCabaña.BuscarCabañaPorTipo(idTipo).Select(cab => new CabañaDTO()
            {
                NumeroHabitacion = cab.NumeroHabitacion,
                Nombre = cab.Nombre.valor,
                Foto = cab.Foto,
                Descripcion = cab.Descripcion.valor,
                PoseeJacuzzi = cab.PoseeJacuzzi,
                EstaHabilitada = cab.EstaHabilitada,
                CantidadPersonasMax = cab.CantidadPersonasMax,
                IdTipoCabaña = cab.IdTipoCabaña

            }); ;
        }
    }
}
