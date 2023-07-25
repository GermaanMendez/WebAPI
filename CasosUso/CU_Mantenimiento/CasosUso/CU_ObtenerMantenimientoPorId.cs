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
    public class CU_ObtenerMantenimientoPorId : IBuscarMantenimientoPorId
    {
        IRepositorioMantenimiento RepoMantenimiento { get; set; }
        public CU_ObtenerMantenimientoPorId(IRepositorioMantenimiento repoMant)
        {
            RepoMantenimiento = repoMant;
        }
        public MantenimientoDTO BuscarPorId(int id)
        {
            MantenimientoDTO mantDTO = null;
            Mantenimiento mant = RepoMantenimiento.FindById(id);

            if (mant != null)
            {
                mantDTO = new MantenimientoDTO()
                {
                    Id = mant.Id,
                    FechaMantenimiento = mant.FechaMantenimiento,
                    Descripcion = mant.Descripcion,
                    CostoMantenimiento = mant.CostoMantenimiento,
                    NombreEmpleado = mant.NombreEmpleado,
                    IdCabaña = mant.IdCabaña,
                 };
            };
            return mantDTO;
        }
    }
}
