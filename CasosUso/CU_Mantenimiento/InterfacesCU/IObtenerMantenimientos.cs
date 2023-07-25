using Dominio_Interfaces.EnitdadesNegocio;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.CU_Mantenimiento.InterfacesCU
{
    public interface IObtenerMantenimientos
    {
        IEnumerable<MantenimientoDTO> ListarTodos();
    }
}
