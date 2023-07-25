using Dominio_Interfaces.EnitdadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.InterfacesRepositorios
{
    public interface IRepositorioMantenimiento:IRepositorio<Mantenimiento>
    {
       IEnumerable<Mantenimiento> ObtenerMantenimientosPorCabaña(int idCabaña);
       IEnumerable<Mantenimiento> ObtenerMantenimientosPorCabañaPorFechas(int idCabaña,DateTime fecha1, DateTime fecha2);

        IEnumerable<Mantenimiento> ObtenerMantenimientosPorValores(double valor1, double valor2);
        
    }
}
