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
    public class CU_ListarMantenimientoPorCabaña : IListarMantenimientoPorCabaña
    {
        IRepositorioMantenimiento RepoMantenimiento { get; set; }
        public CU_ListarMantenimientoPorCabaña(IRepositorioMantenimiento repo)
        {
            RepoMantenimiento = repo;
        }
        public IEnumerable<MantenimientoDTO> ListarPorCabaña(int idCabaña)
        {
            return RepoMantenimiento.ObtenerMantenimientosPorCabaña(idCabaña).Select(mant => new MantenimientoDTO()
            {
                Id = mant.Id,
                FechaMantenimiento = mant.FechaMantenimiento,
                Descripcion = mant.Descripcion,
                CostoMantenimiento = mant.CostoMantenimiento,
                NombreEmpleado = mant.NombreEmpleado,
                IdCabaña = mant.IdCabaña,
            }); 
        }
    }
}
