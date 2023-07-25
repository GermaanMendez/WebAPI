using CasosUso.CU_Cabaña.InterfacesCU;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Cabaña.CasosUso
{
    public class CU_ListarPorTipoYMonto : IListarPorTipoYMonto
    {
        public IRepositorioCabaña repoCabaña { get; set; }
        public CU_ListarPorTipoYMonto(IRepositorioCabaña rep)
        {
            repoCabaña = rep;
        }
        public IEnumerable<CabañaDTO> ListarCabañasPorTipoYMonto(double monto) {

            return repoCabaña.ObtenerCabañasPorTipoYMonto( monto).Select(cab => new CabañaDTO()
            {
                NumeroHabitacion = cab.NumeroHabitacion,
                Nombre = cab.Nombre.valor,
                Foto = cab.Foto,
                Descripcion = cab.Descripcion.valor,
                PoseeJacuzzi = cab.PoseeJacuzzi,
                EstaHabilitada = cab.EstaHabilitada,
                CantidadPersonasMax = cab.CantidadPersonasMax,
                IdTipoCabaña = cab.IdTipoCabaña
            });
        }
    }
}
