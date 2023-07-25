using Dominio_Interfaces.EnitdadesNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.InterfacesRepositorios
{
    public interface IRepositorioTipoCabaña:IRepositorio<TipoCabaña>
    {
        TipoCabaña BuscarTipoPorNombre(string nombre);
        List<TipoCabaña> GetAllLista();
    }
}
