using Dominio_Interfaces.EnitdadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dominio_Interfaces.InterfacesRepositorios
{
    public  interface IRepositorioCabaña:IRepositorio<Cabaña>
    {
        IEnumerable<Cabaña> BuscarCabañaPorTexto(string texto);
        IEnumerable<Cabaña> BuscarCabañaPorTipo(int idTipoCabaña);
        IEnumerable<Cabaña> BuscarCabañaPorCantPersonas(int cantPersonas);
        IEnumerable<Cabaña> ObtenerCabañasNOHabilitadas();

        IEnumerable<Cabaña> ObtenerCabañasMonto( int monto);
        IEnumerable<Cabaña> ObtenerCabañasDisponiblesEnRangoFecha(DateTime desde, DateTime hasta);
        bool EliminarCabaña(string emailDueño, int idCabaña);
        bool DeshabilitarCabaña(string emailDueño, int idCabaña);
        bool HabilitarCabaña(string emailDueño, int idCabaña);

    }
}
