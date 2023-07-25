using CasosUso.CU_Mantenimiento.InterfacesCU;
using Dominio_Interfaces.EnitdadesNegocio;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Mantenimiento.CasosUso
{
    public class CU_ListarMantenimientoPorCabañaYFecha:IListarMantenimientoPorCabañaYFecha
    {
        IRepositorioMantenimiento RepoMantenimiento { get; set; }
        public CU_ListarMantenimientoPorCabañaYFecha(IRepositorioMantenimiento repo)
        {
            RepoMantenimiento = repo;
        }

        public IEnumerable<MantenimientoDTO> ListarPorCabañaYFecha(int idCabaña, DateTime fecha1, DateTime fecha2)
        {
            return RepoMantenimiento.ObtenerMantenimientosPorCabañaPorFechas(idCabaña, fecha1, fecha2).Select(mant => new MantenimientoDTO()
            {
                Id = mant.Id,
                FechaMantenimiento = mant.FechaMantenimiento,
                Descripcion = mant.Descripcion,
                CostoMantenimiento = mant.CostoMantenimiento,
                NombreEmpleado = mant.NombreEmpleado,
                IdCabaña = mant.IdCabaña,
            }); ;
        }
    }
}
