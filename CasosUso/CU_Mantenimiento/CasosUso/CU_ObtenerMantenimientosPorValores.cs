using CasosUso.CU_Mantenimiento.InterfacesCU;
using Dominio_Interfaces.InterfacesRepositorios;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Mantenimiento.CasosUso
{
    public class CU_ObtenerMantenimientosPorValores : IObtenerMantenimientosPorValores
    {
        IRepositorioMantenimiento repoMant { get; set; }
        public CU_ObtenerMantenimientosPorValores(IRepositorioMantenimiento rep)
        {
            repoMant = rep;
        }

        public IEnumerable<MantenimientoDTO> ListarTrabajoEmpleadoPorValores(double valor1, double valor2)
        {
            var mantenimientos = repoMant.ObtenerMantenimientosPorValores(valor1, valor2);

            var resultado = mantenimientos.GroupBy(mant => mant.NombreEmpleado)
                .Select(grupo => new MantenimientoDTO()
                {
                    NombreEmpleado = grupo.Key,
                    CostoMantenimiento = grupo.Sum(mant => mant.CostoMantenimiento),
                });

            return resultado;
        }

    }
}
