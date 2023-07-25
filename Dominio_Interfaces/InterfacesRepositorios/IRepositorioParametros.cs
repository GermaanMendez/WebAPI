using Dominio_Interfaces.EntidadesAuxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.InterfacesRepositorios
{
    public interface IRepositorioParametros:IRepositorio<Parametros>
    {
        string ValorParametro(string nombre);
    }
}
