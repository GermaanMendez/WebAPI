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
    public class CU_ObtenerMantenimientos:IObtenerMantenimientos
    {
        IRepositorioMantenimiento RepoMantenimiento { get; set; }
        public CU_ObtenerMantenimientos(IRepositorioMantenimiento repo)
        {
            RepoMantenimiento = repo;
        }

        public IEnumerable<MantenimientoDTO> ListarTodos()
        {
            return RepoMantenimiento.GetAll().Select(mant => new MantenimientoDTO()
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
